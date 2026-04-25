using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("StartMenu"); // loads first scene
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // shows in editor
        Application.Quit();     // works in build
    }
}