using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectPooler objectPooler;
    public string unitName;
    public int Cost;
    public int Attack;
    public int HP;
    public int Defence;
    public int _maxHealth;
    public int _maxDef;
    
    //public int health;
    

    public void Awake()
    {
        objectPooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
        
    }

    public void Spawn()
    {
        CardScr card = GetComponent<CardScr>();
        objectPooler.SpawnFromPool(unitName, card.defaultPos, Quaternion.identity, card.parent);
    }

    // public class Memento
    // {
    //     int attack;
    //     int health;
    //     int def;
    //     public Memento(int _attack, int _hp, int _def)
    //     {
    //         Attack = _attack;
    //         Health = _hp;
    //         Def = _def;
    //     }
    //     public int Attack
    //     {
    //         get => this.attack;
    //     }
    //     public int Health
    //     {
    //         get => this.health;
    //     }
    //     public int Def
    //     {
    //         get => this.def;
    //     }
    // }
    
}
