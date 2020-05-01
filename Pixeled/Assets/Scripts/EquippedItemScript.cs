using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemScript : MonoBehaviour
{
    protected GameObject equippedItem;
    protected PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerController>();

        equippedItem = GameObject.Find("EquippedItem");
    }

    public void UpdateEquippedItem(string imagePath)
    {
        if (!equippedItem.activeInHierarchy)
        {
            equippedItem.SetActive(true);
        }
        equippedItem.GetComponent<Image>().sprite = Resources.Load<GameObject>("Prefabs/" + imagePath).GetComponent<SpriteRenderer>().sprite;
    }

    public void EmptyEquippedItem()
    {
        if (equippedItem.activeInHierarchy)
        {
            equippedItem.SetActive(false);
            equippedItem.GetComponent<Image>().sprite = null;
        }
    }
}
