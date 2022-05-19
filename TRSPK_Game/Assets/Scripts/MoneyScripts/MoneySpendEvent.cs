using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendMoneyEventArgs: EventArgs
{
    public int Spent;
}

public class SpendMoneyEvent
{
    public static event EventHandler<SpendMoneyEventArgs> SME;
    public void OnSpendMoney(int sp)
    {
        SpendMoneyEventArgs m = new SpendMoneyEventArgs();

        if (SME != null)
        {
            m.Spent = sp;
            SME(this, m);
        }
    }
}

