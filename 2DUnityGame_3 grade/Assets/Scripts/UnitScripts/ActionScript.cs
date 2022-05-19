using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ActionScript : MonoBehaviour
{
    public event Action<int> Damage = delegate { };
    public event Action Killed = delegate { };
    public event Action Clone = delegate { };
    public event Action<GameObject, GameObject, int, TMP_Text> SpecAction = delegate { };
    public event Action<int> Heal = delegate {  };

    public int currentHealth;
    public int currentDefence;

    public void TakeDamage(int damage)
    {
        Damage.Invoke(damage);
        
        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Killed.Invoke();
    }
    public void Ultimate(GameObject field, GameObject enemyField, int index, TMP_Text textBox)
    {
        SpecAction.Invoke(field, enemyField, index, textBox);
    }

    public void ToClone()
    {
        Clone.Invoke();
    }

    public void HealUnit(int amount)
    {
        Heal.Invoke(amount);
    }

}
