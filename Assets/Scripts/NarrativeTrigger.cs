using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    public GameObject textUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        textUI.SetActive(false);
    }
}