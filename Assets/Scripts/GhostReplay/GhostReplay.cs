using System.Collections.Generic;
using UnityEngine;

public class GhostReplay : MonoBehaviour
{
    private List<Vector3> positions;
    private int index = 0;

    public void Initialize(List<Vector3> recordedPositions)
    {
        positions = new List<Vector3>(recordedPositions);
    }

    void Update()
    {
        if (positions == null || index >= positions.Count)
            return;

        transform.position = positions[index];
        index++;
    }
}