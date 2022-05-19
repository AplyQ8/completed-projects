using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class ArcherScr:  MonoBehaviour, IEndDragHandler, IBeginDragHandler, IUnit, IPrototype, IHeal
{
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
    
    [SerializeField] int ArcherId = 2;
    [SerializeField] string ArcherName = "Archer";
    [SerializeField] int ArcherHitPoints = 25;
    [SerializeField] int ArcherAttack = 30;
    [SerializeField] int ArcherDefence = 15;
    [SerializeField] int ArcherCost = 20;
    [SerializeField] private int _maxHealth = 25;
    [SerializeField] private int _maxDef = 15;
    [SerializeField] private string ArcherDescription;
    public int UnitId { get { return ArcherId; } set { ArcherId = value; } }
    public string UnitName { get { return ArcherName; } set { ArcherName = value; } }
    public int UnitHitPoints { get { return ArcherHitPoints; } set { ArcherHitPoints = value; } }
    public int UnitAttack { get { return ArcherAttack; } set { ArcherAttack = value; } }
    public int UnitDefence { get { return ArcherDefence; } set { ArcherDefence = value; } }
    public int UnitCost { get { return ArcherCost; } set { ArcherCost = value; } }
    
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

        ArcherDescription = "Shoots another random unit with some chance";
        
        spawner._maxHealth = _maxHealth;
        spawner._maxDef = _maxDef;
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
        Description.text = ArcherDescription.ToString();
        UnitDefence = spawner.Defence;
        UnitHitPoints = spawner.HP;
        healthBar.SetHealth(UnitHitPoints);
        defBar.SetHealth(UnitDefence);
        healthBar.SetMaxHealth(spawner._maxHealth);
        defBar.SetMaxHealth(spawner._maxDef);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        //ArrayUnits units = new ArrayUnits();
        
        CardScr card = GetComponent<CardScr>();
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
            //ArcherDefence = Action.currentDefence;
            UnitDefence -= damage;
            GetComponent<Spawner>().Defence = UnitDefence;
            
        }
        else
        {
            //ArcherHitPoints = Action.currentHealth;
            UnitHitPoints -= damage;
            Action.currentHealth = UnitHitPoints;
            GetComponent<Spawner>().HP = UnitHitPoints;
        }
    }

    private void OnEnable() {
        Action.Damage += TakeDamage;
        Action.Killed += Kill;
        Action.SpecAction += Ultimate;
        Action.Clone += Clone;
        Action.Heal += Heal;

    }
    private void OnDisable() {
        Action.Damage -= TakeDamage;
        Action.Killed -= Kill;
        Action.SpecAction -= Ultimate;
        Action.Clone -= Clone;
        Action.Heal -= Heal;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public void Ultimate(GameObject field, GameObject enemyField, int index, TMP_Text textBox)
    {
        System.Random rand = new System.Random();
        int coin = rand.Next(0, 101);
        if(coin > 50)
        {
            List<GameObject> units = new List<GameObject>();
            for(int i = 0; i < enemyField.transform.childCount; i++)
            {
                if(enemyField.transform.GetChild(i).transform.childCount > 0)
                {
                    units.Add(enemyField.transform.GetChild(i).GetChild(0).gameObject);
                }
            }
            GameObject unit = units[rand.Next(0, units.Count)];
            ActionScript enemyAction = unit.GetComponent<ActionScript>();
            enemyAction?.TakeDamage(UnitAttack);
            textBox.text +=
                $"Archers action was activated and dealed {UnitAttack} damage a {unit.GetComponent<Spawner>().unitName}\n";

            units.Clear();
        }
    }


    public void Clone()
    {
        GameObject _parent = gameObject.transform.parent.gameObject;
        GameObject newArcher = Instantiate(_parent, 
            gameObject.transform.position,
            gameObject.transform.rotation,
            _parent.transform.parent);
    }
    public void Heal(int heal)
    {
        UnitHitPoints += heal;
        if (UnitHitPoints > spawner._maxHealth)
            UnitHitPoints = spawner._maxHealth;
    }
    
}

