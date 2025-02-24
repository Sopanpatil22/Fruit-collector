using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Main_Menu : MonoBehaviour
{
    private bool isPaused = false;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;  // Pause (0) / Resume (1)
    }


    public void ExitGame()
    {
        Application.Quit();

        // Stop play mode if running in Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
