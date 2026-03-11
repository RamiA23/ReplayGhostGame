using UnityEngine;

/// <summary>
/// Attach to the Player. Casts a short ray forward from the camera; if the player
/// looks at an ActivatableConsole and presses E, it activates.
/// (Step 4: Player Impact on the World)
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    public float interactRange = 2.5f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Prompt (Optional)")]
    public TMPro.TextMeshProUGUI promptText;   // small "Press E" hint on HUD
    public string promptMessage = "[E] Activate";

    private ActivatableConsole currentTarget;

    void Update()
    {
        RaycastHit hit;
        bool foundTarget = Physics.Raycast(
            playerCamera.transform.position,
            playerCamera.transform.forward,
            out hit,
            interactRange
        );

        ActivatableConsole console = foundTarget ? hit.collider.GetComponent<ActivatableConsole>() : null;

        if (console != null && !console.IsActivated)
        {
            currentTarget = console;
            ShowPrompt(true);

            if (Input.GetKeyDown(interactKey))
                currentTarget.Activate();
        }
        else
        {
            currentTarget = null;
            ShowPrompt(false);
        }
    }

    void ShowPrompt(bool visible)
    {
        if (promptText == null) return;
        promptText.gameObject.SetActive(visible);
        if (visible) promptText.text = promptMessage;
    }
}
