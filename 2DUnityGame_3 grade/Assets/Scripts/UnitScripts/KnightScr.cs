using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Random = System.Random;

public class KnightScr : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IUnit, IHeal
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Defence;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Description;

    public HealthBarScript healthBar;
    public DefenceBarScript defBar;
    private HelmetBuff _helmet;
    private SpearBuff _spear;
    private ShieldBuff _shield;
    private HorseBuff _horse;
    private Random rand = new Random();
    
    //F_Knight fabric = new F_Knight();
    ActionScript Action;
    private Spawner spawner;
    
    [SerializeField] int KnightId = 5;
    [SerializeField] string KnightName = "Knight";
    [SerializeField] int KnightHitPoints = 45;
    [SerializeField] int KnightAttack = 45;
    [SerializeField] int KnightDefence = 70;
    [SerializeField] int KnightCost = 60;
    [SerializeField] private int _maxHealth = 45;
    [SerializeField] private int _maxDef = 70;
    private string KnightDescription;
    public int UnitId { get { return KnightId; } set { KnightId = value; } }
    public string UnitName { get { return KnightName; } set { KnightName = value; } }
    public int UnitHitPoints { get { return KnightHitPoints; } set { KnightHitPoints = value; } }
    public int UnitAttack { get { return KnightAttack; } set { KnightAttack = value; } }
    public int UnitDefence { get { return KnightDefence; } set { KnightDefence = value; } }
    public int UnitCost { get { return KnightCost; } set { KnightCost = value; } }

    private void Awake()
    {
        Action = GetComponent<ActionScript>();
        spawner = GetComponent<Spawner>();
        _helmet = GetComponent<HelmetBuff>();
        _horse = GetComponent<HorseBuff>();
        _shield = GetComponent<ShieldBuff>();
        _spear = GetComponent<SpearBuff>();
        GetComponent<Spawner>().Cost = UnitCost;
        GetComponent<Spawner>().Attack = UnitAttack;
        GetComponent<Spawner>().HP = UnitHitPoints;
        GetComponent<Spawner>().Defence = UnitDefence;
        GetComponent<Spawner>().unitName = UnitName;
        spawner._maxHealth = _maxHealth;
        spawner._maxDef = _maxDef;
        
        Action.currentDefence = UnitDefence;
        Action.currentHealth = UnitHitPoints;

        KnightDescription = "Can be buffed by nearby warriors";

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
        Description.text = KnightDescription.ToString();
        UnitDefence = spawner.Defence;
        UnitHitPoints = spawner.HP;
        UnitAttack = spawner.Attack;
        
        healthBar.SetMaxHealth(spawner._maxHealth);
        defBar.SetMaxHealth(spawner._maxDef);
        
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
            //KnightDefence = Action.currentDefence;
            UnitDefence -= damage;
            GetComponent<Spawner>().Defence = UnitDefence;
            int buff = rand.Next(0, 5);
            switch (buff)
            {
                case 1:
                {
                    _helmet.Knock();
                    break;
                }
                case 2:
                {
                    _horse.Knock();
                    break;
                }
                case 3:
                {
                    _shield.Knock();
                    break;
                }
                case 4:
                {
                    _spear.Knock();
                    break;
                }
            }

        }
        else
        {
            //KnightHitPoints = Action.currentHealth;
            UnitHitPoints -= damage;
            Action.currentHealth = UnitHitPoints;
            GetComponent<Spawner>().HP = UnitHitPoints;
            int buff = rand.Next(0, 3);
            switch (buff)
            {
                case 1:
                {
                    _horse.Knock();
                    break;
                }
                case 2:
                {
                    _spear.Knock();
                    break;
                }
            }
        }
    }
    private void OnEnable() {
        Action.Damage += TakeDamage;
        Action.Killed += Kill;
        Action.Heal += Heal;
    }
    private void OnDisable() {
        Action.Damage -= TakeDamage;
        Action.Killed -= Kill;
        Action.Heal -= Heal;
    }
    public void Kill()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public void Heal(int heal)
    {
        UnitHitPoints += heal;
        if (UnitHitPoints > spawner._maxHealth)
            UnitHitPoints = spawner._maxHealth;
    }
}
