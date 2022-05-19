using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money : MonoBehaviour
{
    public int origMoney;
    public int AmountOfMoney;
    
    public Text AmountOfMoneyText;
    public bool isEnough;
    private void Start()
    {

        
        SpendMoneyEvent.SME += SpendMoney;
        AmountOfMoneyText.text = AmountOfMoney.ToString();
        origMoney = AmountOfMoney;
        
    }
    private void Update()
    {
        AmountOfMoneyText.text = AmountOfMoney.ToString();
    }

    public void SpendMoney(object sender, SpendMoneyEventArgs e)
    {
        if (AmountOfMoney - e.Spent < 0)
        {
            isEnough = false;
            
            return;
        }
        AmountOfMoney -= e.Spent;
        
        //Debug.Log("Called from moneyScript");
    }
}

