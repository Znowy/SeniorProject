using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    protected Text moneyText;
    protected PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerController>();

        moneyText = GameObject.Find("MoneyAmount").GetComponent<Text>();
    }

    public void UpdateMoneyText(int AmountOfMoney)
    {
        moneyText.text = AmountOfMoney.ToString();
    }
}
