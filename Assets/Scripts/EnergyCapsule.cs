using UnityEngine;

public class EnergyCapsule : MonoBehaviour
{
    public float energyAmount = 15f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<EnergySystem>().AddEnergy(energyAmount);
            Destroy(gameObject);
        }
    }
}