using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI; // assign your pause menu panel in inspector

    private bool isPaused = false;

    void Start()
    {
        Resume(); // ensure game starts unpaused
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isPaused = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // scene 0 = main menu
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // works in editor
        Application.Quit();     // works in build
    }
}