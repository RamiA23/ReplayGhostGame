using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public Transform player;
    public GameObject ghostPrefab;
    public Transform checkpoint;
    public SparkGuide spark;

    private List<Vector3> recordedPositions = new List<Vector3>();
    private bool isRewinding = false;
    private GameObject currentGhost;

    void Update()
    {
        if (!isRewinding)
        {
            Record();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Rewind();
        }
    }

    void Record()
    {
        recordedPositions.Add(player.position);
    }

    void Rewind()
    {
        if (recordedPositions.Count == 0)
            return;

        isRewinding = true;

        // Destroy old ghost
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }

        Vector3 spawnPos = recordedPositions[0];

        currentGhost = Instantiate(ghostPrefab, spawnPos, Quaternion.identity);
        currentGhost.AddComponent<GhostReplay>().Initialize(recordedPositions);

        player.position = checkpoint.position;
        spark.ResetSpark();

        recordedPositions = new List<Vector3>();
        isRewinding = false;
    }
}