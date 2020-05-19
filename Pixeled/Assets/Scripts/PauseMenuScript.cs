using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    GameObject PauseMenu;
    GameObject ControlsMenu;
    GameObject BasicMenu;

    private bool _isGamePaused;
    public bool isGamePaused
    {
        get
        {
            return _isGamePaused;
        }
        set
        {
            if (_isGamePaused != value)
            {
                _isGamePaused = value;
                
                if (_isGamePaused)
                {
                    PauseGame();
                }
                else
                {
                    Resume();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu = GameObject.Find("PauseMenu");
        ControlsMenu = GameObject.Find("ControlsMenu");
        BasicMenu = GameObject.Find("BasicMenu");

        PauseMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        isGamePaused = false;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Back();
        PauseMenu.SetActive(false);
        _isGamePaused = false;
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
