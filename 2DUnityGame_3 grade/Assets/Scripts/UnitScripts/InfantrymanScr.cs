using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Random = System.Random;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class InfantrymanScr : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IUnit, IHeal
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Defence;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Description;
    private Spawner spawner;

    public HealthBarScript healthBar;
    public DefenceBarScript defBar;
    
    ActionScript Action;

    [SerializeField] int InfantrymanId = 1;
    [SerializeField] string InfantrymanName = "Warrior";
    [SerializeField] int InfantrymanHitPoints = 20;
    [SerializeField] int InfantrymanAttack = 10;
    [SerializeField] int InfantrymanDefence = 25;
    [SerializeField] int InfantrymanCost = 15;
    [SerializeField] private string InfantrymanDescription;
    [SerializeField] private int _maxHealth = 20;
    [SerializeField] private int _maxDef = 25;


    public int UnitId { get { return InfantrymanId; } set { InfantrymanId = value; } }
    public string UnitName { get { return InfantrymanName; } set { InfantrymanName = value; } }
    public int UnitHitPoints { get { return InfantrymanHitPoints; } set { InfantrymanHitPoints = value; } }
    public int UnitAttack { get { return InfantrymanAttack; } set { InfantrymanAttack = value; } }
    public int UnitDefence { get { return InfantrymanDefence; } set { InfantrymanDefence = value; } }
    public int UnitCost { get { return InfantrymanCost; } set { InfantrymanCost = value; } }

    
    
        
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

        InfantrymanDescription = "Buffs nearby Knights with some chance";
        
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
        Description.text = InfantrymanDescription.ToString();

        UnitHitPoints = spawner.HP;
        UnitDefence = spawner.Defence;
        
        healthBar.SetHealth(UnitHitPoints);
        defBar.SetHealth(UnitDefence);
        healthBar.SetMaxHealth(spawner._maxHealth);
        defBar.SetMaxHealth(spawner._maxDef);
    }


    //F_Warrior fabric = new F_Warrior();

    

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
            //InfantrymanDefence = Action.currentDefence;
            UnitDefence -= damage;
            GetComponent<Spawner>().Defence = UnitDefence;
        }
        else
        {
            //InfantrymanHitPoints = Action.currentHealth;
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
        GameObject neigbour1 = null;
        GameObject neigbour2 = null;
        //List<GameObject> buffs = new List<GameObject>();
        if (index < field.transform.childCount - 1 && 
            field.transform.GetChild(index + 1).transform.childCount > 0 &&
            field.transform.GetChild(index + 1).GetChild(0).gameObject.activeSelf &&
            field.transform.GetChild(index + 1).GetChild(0).GetComponent<BuffAvailabilityScript>().couldBeBuffed)
        {
            neigbour1 = field.transform.GetChild(index + 1).GetChild(0).gameObject;
        }
        if (index > 0 && 
            field.transform.GetChild(index - 1).transform.childCount > 0 &&
            field.transform.GetChild(index - 1).GetChild(0).gameObject.activeSelf &&
            field.transform.GetChild(index - 1).GetChild(0).GetComponent<BuffAvailabilityScript>().couldBeBuffed)
        {
            neigbour2 = field.transform.GetChild(index - 1).GetChild(0).gameObject;
        }
        Random rand = new Random();
        int knight = rand.Next(0, 2);
        switch (knight)
        {
            case 0:
            {
                if (neigbour1 != null)
                {
                    int coin = rand.Next(0, 4);
                    switch (coin)
                    {
                        case 0:
                        {
                            neigbour1.GetComponent<States>().helmet.Wear();
                            textBox.text += $"Warrior wore a helmet!\n";
                            break;
                        }
                        case 1:
                        {
                            neigbour1.GetComponent<States>().shield.Wear();
                            textBox.text += $"Warrior wore a shield!\n";
                            break;
                        }
                        case 2:
                        {
                            neigbour1.GetComponent<States>().spear.Wear();
                            textBox.text += $"Warrior wore a spear!\n"; 
                            break;
                        }
                        case 3:
                        {
                            neigbour1.GetComponent<States>().horse.Wear();
                            textBox.text += $"Warrior wore a horse!\n"; 
                            break;
                        }
                    }
                    Debug.Log("Warrior called his spec action!");
                    return;
                }
                if (neigbour2 != null)
                {
                    int coin = rand.Next(0, 4);
                    switch (coin)
                    {
                        case 0:
                        {
                            neigbour2.GetComponent<States>().helmet.Wear();
                            textBox.text += $"Warrior wore a helmet!\n"; 
                            break;
                        }
                        case 1:
                        {
                            neigbour2.GetComponent<States>().shield.Wear();
                            textBox.text += $"Warrior wore a shield!\n"; 
                            break;
                        }
                        case 2:
                        {
                            neigbour2.GetComponent<States>().spear.Wear();
                            textBox.text += $"Warrior wore a spear!\n"; 
                            break;
                        }
                        case 3:
                        {
                            neigbour2.GetComponent<States>().horse.Wear();
                            textBox.text += $"Warrior wore a horse!\n"; 
                            break;
                        }
                    }
                    Debug.Log("Warrior called his spec action!");
                    return;
                }
                break;
            }
            case 1:
            {
                if (neigbour1 != null)
                {
                    int coin = rand.Next(0, 5);
                    switch (coin)
                    {
                        case 1:
                        {
                            neigbour1.GetComponent<States>().helmet.Wear();
                            textBox.text += $"Warrior wore a helmet!\n"; 
                            break;
                        }
                        case 2:
                        {
                            neigbour1.GetComponent<States>().shield.Wear();
                            textBox.text += $"Warrior wore a shield!\n"; 
                            break;
                        }
                        case 3:
                        {
                            neigbour1.GetComponent<States>().spear.Wear();
                            textBox.text += $"Warrior wore a spear!\n"; 
                            break;
                        }
                        case 4:
                        {
                            neigbour1.GetComponent<States>().horse.Wear();
                            textBox.text += $"Warrior wore a horse!\n"; 
                            break;
                        }
                    }
                    Debug.Log("Warrior called his spec action!");
                    return;
                }
                if (neigbour2 != null)
                {
                    int coin = rand.Next(0, 5);
                    switch (coin)
                    {
                        case 1:
                        {
                            neigbour2.GetComponent<States>().helmet.Wear();
                            textBox.text += $"Warrior wore a helmet!\n"; 
                            break;
                        }
                        case 2:
                        {
                            neigbour2.GetComponent<States>().shield.Wear();
                            textBox.text += $"Warrior wore a shield!\n"; 
                            break;
                        }
                        case 3:
                        {
                            neigbour2.GetComponent<States>().spear.Wear(); 
                            textBox.text += $"Warrior wore a spear!\n"; 
                            break;
                        }
                        case 4:
                        {
                            neigbour2.GetComponent<States>().horse.Wear();
                            textBox.text += $"Warrior wore a horse!\n"; 
                            break;
                        }
                    }
                    Debug.Log("Warrior called his spec action!");
                    return;
                }
                break;
            }
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
