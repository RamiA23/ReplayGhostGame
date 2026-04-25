using UnityEngine;

public class SwordBlock : MonoBehaviour
{
    public Transform sword;

    float rotationSpeed = 10f;
    Quaternion targetRotation;

    public int currentBlockDirection = -1;

    //  Audio
    public AudioSource audioSource;
    public AudioClip[] effortSounds; // drag 4 clips in inspector

    void Start()
    {
        targetRotation = sword.localRotation;
    }

    void Update()
    {
        HandleInput();

        sword.localRotation = Quaternion.Lerp(
            sword.localRotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            currentBlockDirection = 1;
            PlayEffortSound();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetRotation = Quaternion.Euler(0, 0, -90);
            currentBlockDirection = 0;
            PlayEffortSound();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetRotation = Quaternion.Euler(-90, 0, 0);
            currentBlockDirection = 2;
            PlayEffortSound();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetRotation = Quaternion.Euler(0, 0, 180);
            currentBlockDirection = 3;
            PlayEffortSound();
        }
    }

    void PlayEffortSound()
    {
        if (effortSounds.Length == 0) return;

        int index = Random.Range(0, effortSounds.Length);
        audioSource.PlayOneShot(effortSounds[index]);
    }
}