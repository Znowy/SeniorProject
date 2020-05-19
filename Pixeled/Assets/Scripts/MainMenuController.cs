using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    GameObject MainMenu;
    GameObject ControlsMenu;
    GameObject BasicMenu;

    private void Start()
    {
        MainMenu = GameObject.Find("MainMenu");
        BasicMenu = GameObject.Find("BasicMenu");
        ControlsMenu = GameObject.Find("ControlsMenu");

        MainMenu.SetActive(true);
        ControlsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Controls()
    {
        BasicMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        ControlsMenu.SetActive(false);
        BasicMenu.SetActive(true);
    }
}
