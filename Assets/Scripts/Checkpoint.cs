using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.instance.SetCheckpoint(transform);

            // reset recording so ghost timeline starts from this checkpoint
            ReplayManager.instance.ResetRecording();
        }
    }
}