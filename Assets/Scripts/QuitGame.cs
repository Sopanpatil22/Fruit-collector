using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public GameObject confirmationPanel; // Assign the confirmation panel in the Inspector

    void Start()
    {
        confirmationPanel.SetActive(false); // Hide the panel initially
    }

    public void ShowQuitConfirmation()
    {
        confirmationPanel.SetActive(true); // Show confirmation box
        Time.timeScale=0f;

    }

    public void CancelQuit()
    {
        confirmationPanel.SetActive(false); // Hide confirmation box
        Time.timeScale=1f;
    }

    public void QuitApplication()
    {
        Debug.Log("Quit button pressed. Exiting game...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
