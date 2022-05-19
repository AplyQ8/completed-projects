using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Random = System.Random;

public class PriestScr : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IUnit, IHeal
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Defence;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Description;

    public HealthBarScript healthBar;
    public DefenceBarScript defBar;
    
    //F_Priest fabric = new F_Priest();
    ActionScript Action;
    private Spawner spawner;
    
    [SerializeField] int HealerId = 4;
    [SerializeField] string HealerName = "Priest";
    [SerializeField] int HealerHitPoints = 35;
    [SerializeField] int HealerAttack = 20;
    [SerializeField] int HealerDefence = 15;
    [SerializeField] int HealerCost = 30;
    [SerializeField] private int _maxHealth = 35;
    [SerializeField] private int _maxDef = 15;
    [SerializeField] private int healAmount =  50;
    private string HealerDescription;
    public int UnitId { get { return HealerId; } set { HealerId = value; } }
    public string UnitName { get { return HealerName; } set { HealerName = value; } }
    public int UnitHitPoints { get { return HealerHitPoints; } set { HealerHitPoints = value; } }
    public int UnitAttack { get { return HealerAttack; } set { HealerAttack = value; } }
    public int UnitDefence { get { return HealerDefence; } set { HealerDefence = value; } }
    public int UnitCost { get { return HealerCost; } set { HealerCost = value; } }
    private Random rand = new Random();
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

        HealerDescription = "Heal nearby units with some chance";
        
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
        Description.text = HealerDescription.ToString();
        UnitDefence = spawner.Defence;
        UnitHitPoints = spawner.HP;
        
        
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
            //PriestDefence = Action.currentDefence;
            UnitDefence -= damage;
            GetComponent<Spawner>().Defence = UnitDefence;
        }
        else
        {
            //PriestHitPoints = Action.currentHealth;
            UnitHitPoints -= damage;
            Action.currentHealth = UnitHitPoints;
            GetComponent<Spawner>().HP = UnitHitPoints;
        }
    }
    private void OnEnable() {
        Action.Damage += TakeDamage;
        Action.Killed += Kill;
        Action.Heal += Heal;
        Action.SpecAction += Ultimate;
    }
    private void OnDisable() {
        Action.Damage -= TakeDamage;
        Action.Killed -= Kill;
        Action.Heal -= Heal;
        Action.SpecAction -= Ultimate;
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
        List<GameObject> unitsToHeal = new List<GameObject>();
        
        if (index < field.transform.childCount - 1 && 
            field.transform.GetChild(index + 1).transform.childCount > 0 &&
            field.transform.GetChild(index + 1).GetChild(0).gameObject.activeSelf)
        {
            neigbour1 = field.transform.GetChild(index + 1).GetChild(0).gameObject;
            unitsToHeal.Add(neigbour1);
        }
        if (index > 0 && 
            field.transform.GetChild(index - 1).transform.childCount > 0 &&
            field.transform.GetChild(index - 1).GetChild(0).gameObject.activeSelf)
        {
            neigbour2 = field.transform.GetChild(index - 1).GetChild(0).gameObject;
            unitsToHeal.Add(neigbour2);
        }

        int coin = rand.Next(0, 101);
        if (coin <= 40)
        {
            foreach (var unit in unitsToHeal)
            {
                ActionScript healAction = unit.GetComponent<ActionScript>();
                healAction?.HealUnit(healAmount);
                textBox.text += $"Priest has healed {unit.GetComponent<Spawner>().unitName}\n";
            }
            
        }
    }

    public void Heal(int heal)
    {
        UnitHitPoints += heal;
        if (UnitHitPoints > spawner._maxHealth)
            UnitHitPoints = spawner._maxHealth;
    }
}
