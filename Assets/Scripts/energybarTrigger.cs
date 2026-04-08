using UnityEngine;

public class EnergyTrigger : MonoBehaviour
{
    public GameObject energyUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            energyUI.SetActive(true);
        }
    }
}