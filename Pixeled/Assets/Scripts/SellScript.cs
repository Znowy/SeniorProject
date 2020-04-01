using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellScript : MonoBehaviour
{
    string[][] cropPrices;

    PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerController>();

        cropPrices = new string[][] { new string[] { "Strawberry", "12", "13" }, new string[] { "Blueberry", "14", "15" } };
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (playerScript.currentlyEquipped.Contains("Crop"))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Debug.Log("Selling...");

                    bool fertilized;
                    string crop = "";
                    if (playerScript.currentlyEquipped.Contains("CropF"))
                    {
                        fertilized = true;
                        crop = playerScript.currentlyEquipped.Replace("_CropF", "");
                    }
                    else
                    {
                        fertilized = false;
                        crop = playerScript.currentlyEquipped.Replace("_Crop", "");
                    }

                    int index = -1;
                    for (int i = 0; i < cropPrices.Length; i++)
                    {
                        if (cropPrices[i][0] == crop)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (fertilized)
                    {
                        playerScript.currentlyEquipped = "none";
                        playerScript.AddMoney(Int32.Parse(cropPrices[index][2]));
                    }
                    else
                    {
                        playerScript.currentlyEquipped = "none";
                        playerScript.AddMoney(Int32.Parse(cropPrices[index][1]));
                    }

                    Debug.Log("Sold!");
                }
            }
        }
    }
}