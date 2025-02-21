using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pausemenu;  // Reference to the pause UI panel
    private static bool isPaused = false;

    void Start()
    {
        Pausemenu.SetActive(false);  // Ensure the pause menu is hidden at start
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Pausemenu.SetActive(true);  // Show pause menu
        Time.timeScale = 0f;        // Stop time (pause)
        isPaused = true;
    }

    public void ResumeGame()
    {
        Pausemenu.SetActive(false); // Hide pause menu
        Time.timeScale = 1f;        // Resume time
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;  // Ensure time is running before switching scenes
        SceneManager.LoadScene('Main Menu'); // Load main menu (replace 1 with correct scene index)
    }

    public void QuitGame()
    {
        Application.Quit();  // Quit the game
    } 
}
