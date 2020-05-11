using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public bool isGamePaused;
    public int gameTime;

    protected Text timeText;

    private float _updateTimer;
    public float updateTimer
    {
        get
        {
            return _updateTimer;
        }
        set
        {
            if (_updateTimer < 60 && value >= 60)
            {
                _updateTimer = 60;
            }
            else if (_updateTimer == 60)
            {
                _updateTimer = value - 60;
                gameTime += 60;
                UpdateTime();
            }
            else
            {
                _updateTimer = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;
        gameTime = 21600;

        timeText = GameObject.Find("TimeDisplay").GetComponent<Text>();
        UpdateTime();

        updateTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused)
        {
            updateTimer += Time.deltaTime;
            Debug.Log("Game Time: " + gameTime + ", Update Timer: " + updateTimer);
        }
    }

    void UpdateTime()
    {
        gameTime %= 86400; // Seconds in a day
        int hours = gameTime / 3600;
        int minutes = (gameTime / 60) - (hours * 60);

        if (hours > 12)
        {
            hours -= 12;
        }
        else if (hours == 0)
        {
            hours = 12;
        }

        timeText.text = hours + ":" + minutes.ToString().PadLeft(2, '0');

        Debug.Log("UPDATED TIME");
    }
}
