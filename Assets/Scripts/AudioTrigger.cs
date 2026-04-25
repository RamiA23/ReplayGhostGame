using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;   // drag your main AudioSource here
    public AudioClip newClip;         // the new sound/music to play

    public float volume = 1f;
    public bool destroyOnTrigger = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && newClip != null)
            {
                audioSource.Stop();              // stop current audio
                audioSource.clip = newClip;      // switch clip
                audioSource.volume = volume;
                audioSource.Play();              // play new audio
            }

            if (destroyOnTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}