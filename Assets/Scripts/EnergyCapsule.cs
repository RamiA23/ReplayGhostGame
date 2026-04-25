using UnityEngine;

public class EnergyCapsule : MonoBehaviour
{
    public float energyAmount = 15f;
    public AudioClip pickupSound;
    public float volume = 1.5f; // increase if still quiet

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add energy
            FindObjectOfType<EnergySystem>().AddEnergy(energyAmount);

            // Play sound at camera (so it's always loud)
            if (pickupSound != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, volume);
            }

            // Destroy capsule
            Destroy(gameObject);
        }
    }
}