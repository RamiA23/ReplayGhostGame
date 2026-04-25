using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static string sceneToLoad;

    public static void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;

        // FORCE immediate switch
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
    }
}