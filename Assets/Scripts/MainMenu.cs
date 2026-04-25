using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Image loading;

    public void PlayGame()
    {
        loading.gameObject.SetActive(true); // fix #1
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return null; // let the UI update first

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}