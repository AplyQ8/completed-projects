using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TumbleWeedAdapeter: TumbleWeed, IUnit, IEndDragHandler, IBeginDragHandler
{
    public int UnitId
    {
        get => _tumbleWeedId;
        set => _tumbleWeedId = value;
    }
    public string UnitName
    {
        get => _tumbleWeedName;
        set => _tumbleWeedName = value;
    }
    public int UnitHitPoints
    {
        get => _tumbleWeedHitPoints;
        set => _tumbleWeedHitPoints = value;
    }
    public int UnitAttack
    {
        get => _tumbleWeedAttack;
        set => _tumbleWeedAttack = value;
    }
    public int UnitDefence
    {
        get => _tumbleWeedDefence;
        set => _tumbleWeedDefence = value;
    }
    public int UnitCost
    {
        get => _tumbleWeedCost;
        set => _tumbleWeedCost = value;
    }
    private string TumbleWeedDescription;
    
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Defence;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Description;

    public HealthBarScript healthBar;
    public DefenceBarScript defBar;
    
    ActionScript Action;
    private Spawner spawner;
    
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _maxDef = 200;
    
    private void Awake()
    {
        Action = GetComponent<ActionScript>();
        spawner = GetComponent<Spawner>();
        GetComponent<Spawner>().Cost = UnitCost;
        GetComponent<Spawner>().Attack = UnitAttack;
        GetComponent<Spawner>().HP = UnitHitPoints;
        GetComponent<Spawner>().Defence = UnitDefence;
        Action.currentDefence = UnitDefence;
        Action.currentHealth = UnitHitPoints;

        TumbleWeedDescription = "Heavy unit with low attack";

        healthBar.SetMaxHealth(_maxHealth);
        defBar.SetMaxHealth(_maxDef);
    }
    private void Update()
    {
        Name.text = UnitName.ToString();
        Attack.text = UnitAttack.ToString();
        Health.text = UnitHitPoints.ToString();
        Defence.text = UnitDefence.ToString();
        Cost.text = UnitCost.ToString();
        Description.text = TumbleWeedDescription.ToString();
        UnitHitPoints = spawner.HP;
        UnitDefence = spawner.Defence;
        healthBar.SetHealth(UnitHitPoints);
        defBar.SetHealth(UnitDefence);

    }
    public void OnEndDrag(PointerEventData eventData)
    {

        CardScr card = GetComponent<CardScr>();
        //ArrayUnits units = new ArrayUnits();
        //Debug.Log("Called From UnitScript");

        if (card.isDropped)
        {
            SlotScript slot = GetComponentInParent<SlotScript>();
            //GameObject.Find("Array").GetComponent<ArrayUnits>().units[slot.cellX, slot.cellY] = fabric.Create();
            //units.units[slot.cellX, slot.cellY] = fabric.Create();

            SpendMoneyEvent sme = new SpendMoneyEvent();
            sme.OnSpendMoney(UnitCost);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        LocalCostScript lc = GameObject.Find("LocalCost").GetComponent<LocalCostScript>();
        lc.cost = UnitCost;
    }
    
    public void TakeDamage(int damage)
    {
        if (UnitDefence > 0)
        {
            //TumbleWeedDefence = Action.currentDefence;
            UnitDefence -= damage;
            GetComponent<Spawner>().Defence = UnitDefence;
        }            
        else
        {
            //TumbleWeedHitPoints = Action.currentHealth;
            UnitHitPoints -= damage;
            Action.currentHealth = UnitHitPoints;
            GetComponent<Spawner>().HP = UnitHitPoints;
            
        }
    }
    
    private void OnEnable() {
        Action.Damage += TakeDamage;
        Action.Killed += Kill;
    }
    private void OnDisable() {
        Action.Damage -= TakeDamage;
        Action.Killed -= Kill;
    }
    public void Kill()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
