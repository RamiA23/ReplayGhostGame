using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScreenFadeAndFight : MonoBehaviour
{
    [Header("Fade UI")]
    public Image fadeImage;
    public float fadeSpeed = 2f;

    [Header("Countdown UI")]
    public TextMeshProUGUI countdownText;
    public float countdownTime = 1f;

    [Header("Characters")]
    public Transform player;
    public Transform enemy;

    [Header("Fight Positions")]
    public Transform playerFightPos;
    public Transform enemyFightPos;

    [Header("Scripts")]
    public MonoBehaviour playerMovementScript;
    public MonoBehaviour enemyAIScript;

    [Header("Combat")]
    public SwordBlock swordBlockScript;

    public float delayBeforeMove = 0.3f;

    public void StartFightTransition()
    {
        StartCoroutine(FadeAndMove());

        // Disable movement immediately
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // Disable sword block at start
        if (swordBlockScript != null)
            swordBlockScript.enabled = false;
    }

    IEnumerator FadeAndMove()
    {
        // Fade to black
        yield return StartCoroutine(Fade(0, 1));

        yield return new WaitForSeconds(delayBeforeMove);

        // Move characters
        player.position = playerFightPos.position;
        enemy.position = enemyFightPos.position;

        // Fix rotation
        player.rotation = playerFightPos.rotation * Quaternion.Euler(0, 180, 0);
        enemy.rotation = enemyFightPos.rotation;

        // Fade back in
        yield return StartCoroutine(Fade(1, 0));

        // Ensure movement is disabled
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // Start countdown
        yield return StartCoroutine(Countdown());

        // Enable enemy AI AFTER countdown
        if (enemyAIScript != null)
            enemyAIScript.enabled = true;
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float t = 0f;
        Color color = fadeImage.color;

        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }

    IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "Defeat the subject in a sword duel, use the arrow keys to block and use x to finish him off when the time is right";
        yield return new WaitForSeconds(10f);

        countdownText.text = "3";
        yield return new WaitForSeconds(countdownTime);

        countdownText.text = "2";
        yield return new WaitForSeconds(countdownTime);

        countdownText.text = "1";
        yield return new WaitForSeconds(countdownTime);

        countdownText.text = "FIGHT!";

        //  Enable sword blocking EXACTLY when fight starts
        if (swordBlockScript != null)
            swordBlockScript.enabled = true;

        yield return new WaitForSeconds(0.8f);

        countdownText.gameObject.SetActive(false);
    }
}