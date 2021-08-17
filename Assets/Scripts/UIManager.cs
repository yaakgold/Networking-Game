using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject connectingPanel;
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;

    public void Awake()
    {
        connectingPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    //panel management
    public void Connection()
    {
        connectingPanel.SetActive(true);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    public void Credits()
    {
        connectingPanel.SetActive(false);
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void MainMenuPanel()
    {
        connectingPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    //game management
    public void QuitGame()
    {
        Application.Quit();
    }
}
