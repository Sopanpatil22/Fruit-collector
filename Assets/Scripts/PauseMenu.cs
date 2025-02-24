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
        Pausemenu.SetActive(false); 
        Time.timeScale = 1f;        
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;  
        SceneManager.LoadScene(0); 
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
