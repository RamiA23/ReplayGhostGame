using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    private Transform currentCheckpoint;

    private void Awake()
    {
        instance = this;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public Vector3 GetCheckpointPosition()
    {
        if (currentCheckpoint != null)
            return currentCheckpoint.position;

        return Vector3.zero;
    }
}