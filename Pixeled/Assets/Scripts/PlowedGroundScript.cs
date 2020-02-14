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
        if (currentlyPlanted == "none")
        {
            if (currentSprite.name != "PlowedGround_" + saturation)
            {
                currentSprite = Resources.Load<Sprite>("Prefabs/PlowedGround_" + saturation);
            }
        }
        else
        {
            growTime += Time.deltaTime;
            Debug.Log(growTime);
            if (currentSprite.name != currentlyPlanted + "_" + saturation + "_" + plantGrowth())
            {
                GetComponent<SpriteRenderer>().sprite = currentSprites[plantGrowth()];
            }
        }
    }

    int plantGrowth()
    {
        int time = Mathf.FloorToInt(growTime);
        time *= growMultiplier;

        if (time >= 400)
            return 4;
        else
            return time / 100;
    }
}
