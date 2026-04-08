using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Transform target;
    private Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = transform.position;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;

        // move toward target
        transform.position += direction * moveSpeed * Time.deltaTime;

        // rotate to face target
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 6f * Time.deltaTime);
        }
    }

    // detect player or ghost entering detection radius
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ghost"))
        {
            target = other.transform;
        }
    }

    // stop chasing if target leaves radius
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            target = null;
        }
    }

    // if enemy touches player → respawn
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RespawnPlayer(collision.gameObject);
        }
    }

    void RespawnPlayer(GameObject player)
    {
        Vector3 spawn = CheckpointManager.instance.GetCheckpointPosition() + Vector3.up * 1f;

        CharacterController controller = player.GetComponent<CharacterController>();

        // disable controller before teleporting
        if (controller != null)
            controller.enabled = false;

        player.transform.position = spawn;

        // re-enable controller
        if (controller != null)
            controller.enabled = true;

        // reset enemy
        transform.position = spawnPosition;
        target = null;
    }
}