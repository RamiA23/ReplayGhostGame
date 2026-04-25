using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    public GameObject textUI;

    private Collider triggerCollider;

    void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            bool isInside = triggerCollider.bounds.Contains(player.transform.position);

            textUI.SetActive(isInside);
        }
    }
}