using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlowedGroundScript : MonoBehaviour
{
    public string currentlyPlanted = "none";
    public float growTime = 0;
    public string saturation = "Dry";
    public bool fertilized = false;
    public int growMultiplier = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerController playerControllerScript = collision.gameObject.GetComponent<PlayerController>();
            if (GetComponent<SpriteRenderer>().sprite.name.Contains("5") && playerControllerScript.currentlyEquipped.Contains("Hoe"))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    resetGround();
                }
            }
            else if (GetComponent<SpriteRenderer>().sprite.name.Contains("4") && playerControllerScript.currentlyEquipped.Contains("none"))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    playerControllerScript.currentlyEquipped = currentlyPlanted.Replace("Seed", "Crop");
                    if (fertilized)
                        playerControllerScript.currentlyEquipped += "F";
                    resetGround();
                }
            }
            else if (playerControllerScript.currentlyEquipped.Contains("Seed") && currentlyPlanted == "none")
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    currentlyPlanted = playerControllerScript.currentlyEquipped;
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Plants/" + playerControllerScript.currentlyEquipped + "_" + saturation);
                }
            }
            else if (playerControllerScript.currentlyEquipped.Contains("Wateringcan") && saturation == "Dry")
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    saturation = "Wet";
                }
            }
            else if (playerControllerScript.currentlyEquipped.Contains("Fertilizer") && fertilized == false)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    fertilized = true;

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Sprite[] currentSprites = Resources.LoadAll<Sprite>("Plants/" + currentlyPlanted + "_" + saturation);
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        if (!GetComponent<SpriteRenderer>().sprite.name.Contains("5")) // [ If plant is not withered ]
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
                {
                    if (fertilized)
                        growTime += Time.deltaTime;
                    growTime += Time.deltaTime;
                }
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

    void resetGround()
    {
        currentlyPlanted = "none";
        saturation = "Dry";
        fertilized = false;
        growTime = 0;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Plants/PlowedGround_Dry");
    }
}
