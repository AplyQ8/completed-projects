using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;
using TMPro;

public class Coin_Flip : MonoBehaviour
{
    private Animator anim;
    public int coin;
    private Random rand = new Random();
    [SerializeField] private TMP_Text text;

    void Awake()
    {
        anim = GetComponent<Animator>();
        coin = rand.Next(0, 2);
        
    }

    public void Flip()
    {
        anim.SetTrigger("ReadyButton");
        switch (coin)
        {
            case 0:
            {
                anim.SetInteger("coin", coin);
                text.text = "First turn is after enemy!";
                break;
            }
            case 1:
            {
                anim.SetInteger("coin", coin);
                text.text = "First turn is after player!";
                break;
            }
        }
    }

    
}
