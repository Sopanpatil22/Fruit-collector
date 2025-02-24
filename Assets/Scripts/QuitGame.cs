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
    }

    public void CancelQuit()
    {
        confirmationPanel.SetActive(false); // Hide confirmation box
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
