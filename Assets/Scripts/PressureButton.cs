using UnityEngine;

public class PressureButton : MonoBehaviour
{
    public GameObject bridge;

    private bool isPressed = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = true;
        }
    }

    private void FixedUpdate()
    {
        // If something is pressing it this physics frame → ON
        if (isPressed)
        {
            bridge.SetActive(true);
        }
        else
        {
            bridge.SetActive(false);
        }

        // Reset for next frame
        isPressed = false;
    }
}