using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Place on a trigger collider zone. When the player enters, displays a short
/// experiment log entry via TextMeshPro and optionally plays an ambient audio clip.
/// Triggers only once per play session.
/// (Step 3: Interactive Narrative Trigger)
/// </summary>
public class NarrativeTrigger : MonoBehaviour
{
    [Header("Log Content")]
    [TextArea(3, 8)]
    public string logText = "EXPERIMENT LOG 7\n\nEcho reconstruction unstable. Residual timelines persisting after shutdown sequence. Do not enter chamber B.";

    [Header("UI Reference")]
    public TextMeshProUGUI displayText;   // assign the TMP text element on your HUD canvas
    public float displayDuration = 6f;
    public float fadeDuration = 0.5f;

    [Header("Audio (Optional)")]
    public AudioSource audioSource;       // assign an AudioSource with a clip pre-loaded

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        StartCoroutine(ShowLog());
    }

    IEnumerator ShowLog()
    {
        if (displayText == null) yield break;

        displayText.text = logText;
        displayText.gameObject.SetActive(true);

        // Fade in
        yield return StartCoroutine(FadeText(0f, 1f, fadeDuration));

        if (audioSource != null)
            audioSource.Play();

        yield return new WaitForSeconds(displayDuration);

        // Fade out
        yield return StartCoroutine(FadeText(1f, 0f, fadeDuration));

        displayText.gameObject.SetActive(false);
    }

    IEnumerator FadeText(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = displayText.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            displayText.color = c;
            yield return null;
        }
        c.a = to;
        displayText.color = c;
    }
}
