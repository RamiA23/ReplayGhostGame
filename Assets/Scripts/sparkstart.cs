using UnityEngine;

public class sparkstart : MonoBehaviour
{
    public GameObject spark;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spark.SetActive(true);
        }
    }
}
