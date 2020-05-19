using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
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
            if (_updateTimer < 6 && value >= 6)
            {
                _updateTimer = 6;
            }
            else if (_updateTimer == 6)
            {
                _updateTimer = value - 6;
                gameTime += 1;
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
        gameTime = 360; // Start at 6

        timeText = GameObject.Find("TimeDisplay").GetComponent<Text>();
        UpdateTime();

        updateTimer = 0;
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate, put physics code here
    void FixedUpdate()
    {
        updateTimer += Time.deltaTime;
        //Debug.Log("Game Time: " + gameTime + ", Update Timer: " + updateTimer);
    }

    void UpdateTime()
    {
        gameTime %= 1440; // Seconds in a day
        int hours = gameTime / 60;
        int minutes = gameTime % 60;

        if (hours > 12)
        {
            hours -= 12;
        }
        else if (hours == 0)
        {
            hours = 12;
        }

        timeText.text = hours + ":" + minutes.ToString().PadLeft(2, '0');
    }
}
