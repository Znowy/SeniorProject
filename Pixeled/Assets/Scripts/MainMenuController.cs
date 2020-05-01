using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    GameObject startButton;
    GameObject exitButton;
    GameObject controlsButton;
    GameObject backButton;
    GameObject pixeledTitle;
    GameObject controlsText;

    bool IsControlsMenu;

    private void Start()
    {
        startButton = GameObject.Find("StartButton");
        exitButton = GameObject.Find("ExitButton");
        controlsButton = GameObject.Find("ControlsButton");
        backButton = GameObject.Find("BackButton");
        pixeledTitle = GameObject.Find("PixeledTitle");
        controlsText = GameObject.Find("ControlsText");

        backButton.SetActive(false);
        controlsText.SetActive(false);

        IsControlsMenu = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ControlsMenu()
    {
        if (IsControlsMenu)
        {
            startButton.SetActive(true);
            exitButton.SetActive(true);
            controlsButton.SetActive(true);
            pixeledTitle.SetActive(true);

            backButton.SetActive(false);
            controlsText.SetActive(false);

            IsControlsMenu = false;
        }
        else
        {
            startButton.SetActive(false);
            exitButton.SetActive(false);
            controlsButton.SetActive(false);
            pixeledTitle.SetActive(false);

            backButton.SetActive(true);
            controlsText.SetActive(true);

            IsControlsMenu = true;
        }
    }

    public void Back()
    {
        if (IsControlsMenu)
            ControlsMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
