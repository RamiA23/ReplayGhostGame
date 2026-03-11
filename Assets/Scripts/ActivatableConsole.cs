using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Place on a prop (terminal, control panel, monitor) in the scene.
/// When the player activates it via PlayerInteract, the console changes its material
/// color, plays a sound, and optionally reveals a narrative text pop-up.
/// Represents a facility system the player can interact with once.
/// (Step 4: Player Impact on the World)
/// </summary>
public class ActivatableConsole : MonoBehaviour
{
    [Header("State")]
    public bool IsActivated { get; private set; } = false;

    [Header("Visual Change")]
    public Renderer consoleRenderer;
    public int materialIndex = 0;
    public Color inactiveColor = new Color(0.1f, 0.1f, 0.1f);
    public Color activeColor = new Color(0.2f, 0.8f, 0.4f);     // green "online" glow
    public Light consoleLight;                                    // optional point light on the prop

    [Header("Audio")]
    public AudioSource audioSource;                               // plays on activation

    [Header("Narrative Log (Optional)")]
    public TextMeshProUGUI logDisplay;
    [TextArea(3, 8)]
    public string activationLog = "TERMINAL RESTORED\n\nEcho stabilization protocol initiated. Stand by — residual timeline count: 4 and rising.";
    public float logDisplayDuration = 7f;
    public float fadeDuration = 0.5f;

    void Start()
    {
        // Set starting color
        if (consoleRenderer != null)
        {
            Material mat = consoleRenderer.materials[materialIndex];
            mat.color = inactiveColor;
            if (mat.HasProperty("_EmissionColor"))
                mat.SetColor("_EmissionColor", Color.black);
        }

        if (consoleLight != null)
            consoleLight.enabled = false;

        if (logDisplay != null)
            logDisplay.gameObject.SetActive(false);
    }

    public void Activate()
    {
        if (IsActivated) return;
        IsActivated = true;

        // Change material color to active state
        if (consoleRenderer != null)
        {
            Material mat = consoleRenderer.materials[materialIndex];
            mat.color = activeColor;
            if (mat.HasProperty("_EmissionColor"))
            {
                mat.SetColor("_EmissionColor", activeColor * 2f);
                mat.EnableKeyword("_EMISSION");
            }
        }

        // Switch on the console light
        if (consoleLight != null)
        {
            consoleLight.enabled = true;
            consoleLight.color = activeColor;
            consoleLight.intensity = 1.2f;
            consoleLight.range = 3f;
        }

        // Play activation sound
        if (audioSource != null)
            audioSource.Play();

        // Show narrative log text
        if (logDisplay != null)
            StartCoroutine(ShowLog());
    }

    IEnumerator ShowLog()
    {
        logDisplay.text = activationLog;
        logDisplay.gameObject.SetActive(true);

        yield return StartCoroutine(FadeText(0f, 1f, fadeDuration));
        yield return new WaitForSeconds(logDisplayDuration);
        yield return StartCoroutine(FadeText(1f, 0f, fadeDuration));

        logDisplay.gameObject.SetActive(false);
    }

    IEnumerator FadeText(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = logDisplay.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            logDisplay.color = c;
            yield return null;
        }
        c.a = to;
        logDisplay.color = c;
    }
}
