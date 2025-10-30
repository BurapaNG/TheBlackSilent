using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public static bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // ถ้าหยุดอยู่ ให้กลับไปเล่นต่อ
                Resume();
            }
            else
            {
                // ถ้าเกมกำลังเล่นอยู่ ให้หยุด
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // เอาไว้ซ่อนหน้าต่างเมนู
        Time.timeScale = 1f;
        isPaused = false;

        // Cursor.lockState = CursorLockMode.Locked
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // การคืนค่าเวลาก่อนโหลดฉากใหม่
    }
}