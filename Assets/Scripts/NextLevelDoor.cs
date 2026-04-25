using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevelDoor : MonoBehaviour
{
    public int nextSceneIndex;
    public GameObject loadingImage; // assign in Inspector

    private bool isLoading = false; // prevents multiple triggers

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLoading)
        {
            isLoading = true;

            Debug.Log("Entering next level");

            loadingImage.SetActive(true);
            StartCoroutine(LoadSceneAsync());
        }
    }

    IEnumerator LoadSceneAsync()
    {
        yield return null; // let the loading image render first

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneIndex);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}