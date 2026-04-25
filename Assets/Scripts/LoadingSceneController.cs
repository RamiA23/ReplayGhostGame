using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    public Slider progressBar;

    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            // Wait until fully loaded
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f); // small delay (optional)
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}