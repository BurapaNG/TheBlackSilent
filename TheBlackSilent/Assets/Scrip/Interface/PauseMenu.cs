using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public static bool isPauseMenu = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Debug.Log("Pass ESC!!!");

            if (isPauseMenu)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPauseMenu = false;
        // Cursor.lockState = CursorLockMode.Locked;
        // โค้ดด้านบนไว้ล็อคเมาส์
    }

    void Pause() 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPauseMenu = true;
        // Cursor.lockState = CursorLockMode.None
        // โค้ดด้านบนไว้ปลดเมาส์
    }

    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void LoadMenu() 
    {
        Time.timeScale = 1f;
        // SceneManager.LoadScene("MainMenu");
    }
}