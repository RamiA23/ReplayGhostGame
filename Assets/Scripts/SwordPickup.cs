using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;
    public ScreenFadeAndFight transitionManager;

    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;

    [Header("Player Sword")]
    public GameObject playerSword; // assign in inspector

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        // make sure player sword is off at start
        if (playerSword != null)
            playerSword.SetActive(false);
    }

    void Update()
    {
        // Float up and down
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotate
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (playerSword != null)
                playerSword.SetActive(true);

            if (transitionManager != null)
                transitionManager.StartFightTransition();

            Destroy(gameObject);
        }
    }
}