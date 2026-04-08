using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    public int nextSceneIndex; // set in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entering next level");

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}