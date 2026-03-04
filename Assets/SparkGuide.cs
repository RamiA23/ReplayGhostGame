using UnityEngine;

public class SparkGuide : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;

    public float moveSpeed = 3f;
    public float waitDistance = 6f;

    public float floatHeight = 0.3f;
    public float floatSpeed = 2f;

    public float swayAmount = 0.3f;
    public float swaySpeed = 1.5f;

    private int currentIndex = 0;
    private Vector3 basePosition;
    private Vector3 velocity;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
            basePosition = transform.position;
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Always calculate base movement toward waypoint
        float playerDistance = Vector3.Distance(player.position, transform.position);

        if (playerDistance <= waitDistance)
        {
            Transform target = waypoints[currentIndex];

            basePosition = Vector3.SmoothDamp(
                basePosition,
                target.position,
                ref velocity,
                0.3f
            );

            if (Vector3.Distance(basePosition, target.position) < 0.4f)
            {
                if (currentIndex < waypoints.Length - 1)
                    currentIndex++;
            }
        }

        // Always apply ambient motion
        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        float swayX = Mathf.Cos(Time.time * swaySpeed) * swayAmount;
        float swayZ = Mathf.Sin(Time.time * swaySpeed * 0.8f) * swayAmount;

        Vector3 ambientOffset = new Vector3(swayX, floatOffset, swayZ);

        transform.position = basePosition + ambientOffset;
    }

    public void ResetSpark()
    {
        currentIndex = 0;
        basePosition = waypoints[0].position;
        transform.position = basePosition;
    }
}