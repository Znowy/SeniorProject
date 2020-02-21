using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlowedGroundScript : MonoBehaviour
{
    public string currentlyPlanted = "none";
    public float growTime = 0;
    public string saturation = "Dry";
    public int growMultiplier = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sprite[] currentSprites = Resources.LoadAll<Sprite>("Plants/" + currentlyPlanted + "_" + saturation);
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        if (!GetComponent<SpriteRenderer>().sprite.name.Contains("5"))
        {
            if (currentlyPlanted == "none")
            {
                if (currentSprite.name != "PlowedGround_" + saturation)
                {
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Plants/PlowedGround_" + saturation);
                }
            }
            else
            {
                if (saturation == "Wet")
                    growTime += Time.deltaTime;
                //Debug.Log(growTime); // DEBUG TIME SHOWN
                if (currentSprite.name != currentlyPlanted + "_" + saturation + "_" + plantGrowth())
                {
                    GetComponent<SpriteRenderer>().sprite = currentSprites[plantGrowth()];
                }
            }
        }
    }

    int plantGrowth()
    {
        int time = Mathf.FloorToInt(growTime);
        time *= growMultiplier;

        if (time >= 800)
            return 5;
        else if (time >= 400)
            return 4;
        else
            return time / 100;
    }
}
