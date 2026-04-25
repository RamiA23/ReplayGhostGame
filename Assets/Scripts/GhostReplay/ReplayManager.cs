using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public Transform player;
    public GameObject ghostPrefab;
    public Transform checkpoint;
    public SparkGuide spark;

    public static ReplayManager instance;

    public EnergySystem energySystem;

    //  NEW: Audio
    public AudioClip rewindSound;
    public float volume = 1.5f;

    private List<Vector3> recordedPositions = new List<Vector3>();
    private bool isRewinding = false;
    private GameObject currentGhost;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isRewinding)
        {
            Record();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TryRewind();
        }
    }

    void Record()
    {
        recordedPositions.Add(player.position);
    }

    void TryRewind()
    {
        if (energySystem == null || energySystem.currentEnergy <= 0)
        {
            Debug.Log("No energy to rewind!");
            return;
        }

        energySystem.UseReplay();

        //  PLAY SOUND HERE
        if (rewindSound != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(rewindSound, Camera.main.transform.position, volume);
        }

        Rewind();
    }

    void Rewind()
    {
        if (recordedPositions.Count == 0)
            return;

        isRewinding = true;

        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }

        Vector3 spawnPos = recordedPositions[0];

        currentGhost = Instantiate(ghostPrefab, spawnPos, Quaternion.identity);
        currentGhost.AddComponent<GhostReplay>().Initialize(recordedPositions);

        player.position = CheckpointManager.instance.GetCheckpointPosition() + Vector3.up * 1.5f;

        spark.ResetSpark();

        recordedPositions = new List<Vector3>();
        isRewinding = false;
    }

    public void ResetRecording()
    {
        recordedPositions.Clear();
    }
}