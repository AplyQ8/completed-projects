using System;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MageScr : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IUnit
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Defence;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Description;

    public HealthBarScript healthBar;
    public DefenceBarScript defBar;
    
    //F_Mage fabric = new F_Mage();
    ActionScript Action;
    private Spawner spawner;
    private Random rand = new Random();
    [SerializeField] int MageId = 3;
    [SerializeField] string MageName = "Mage";
    [SerializeField] int MageHitPoints = 25;
    [SerializeField] int MageAttack = 70;
    [SerializeField] int MageDefence = 20;
    [SerializeField] int MageCost = 60;
    [SerializeField] private string MageDescription;
    [SerializeField] private int _maxHealth = 25;
    [SerializeField] private int _maxDef = 20;

    public int UnitId { get { return MageId; } set { MageId = value; } }
    public string UnitName { get { return MageName; } set { MageName = value; } }
    public int UnitHitPoints { get { return MageHitPoints; } set { MageHitPoints = value; } }
    public int UnitAttack { get { return MageAttack; } set { MageAttack = value; } }
    public int UnitDefence { get { return MageDefence; } set { MageDefence = value; } }
    public int UnitCost { get { return MageCost; } set { MageCost = value; } }

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

        MageDescription = "Clone nearby units with some chance";
        
        
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
        Description.text = MageDescription.ToString();
        UnitDefence = spawner.Defence;
        UnitHitPoints = spawner.HP;
        healthBar.SetHealth(UnitHitPoints);
        defBar.SetHealth(UnitDefence);
        healthBar.SetMaxHealth(spawner._maxHealth);
        defBar.SetMaxHealth(spawner._maxDef);
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
            //MageDefence = Action.currentDefence;
            UnitDefence -= damage;
            GetComponent<Spawner>().Defence = UnitDefence;
        }
        else
        {
            //MageHitPoints = Action.currentHealth;
            UnitHitPoints -= damage;
            Action.currentHealth = UnitHitPoints;
            GetComponent<Spawner>().HP = UnitHitPoints;
        }
    }
    private void OnEnable() {
        Action.Damage += TakeDamage;
        Action.Killed += Kill;
        Action.SpecAction += Ultimate;
    }
    private void OnDisable() {
        Action.Damage -= TakeDamage;
        Action.Killed -= Kill;
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
        List<GameObject> unitsToClone = new List<GameObject>();
        
        if (index < field.transform.childCount - 1 && 
            field.transform.GetChild(index + 1).transform.childCount > 0 &&
            field.transform.GetChild(index + 1).GetChild(0).gameObject.activeSelf)
        {
            neigbour1 = field.transform.GetChild(index + 1).GetChild(0).gameObject;
            unitsToClone.Add(neigbour1);
        }
        if (index > 0 && 
            field.transform.GetChild(index - 1).transform.childCount > 0 &&
            field.transform.GetChild(index - 1).GetChild(0).gameObject.activeSelf)
        {
            neigbour2 = field.transform.GetChild(index - 1).GetChild(0).gameObject;
            unitsToClone.Add(neigbour2);
        }

        int coin = rand.Next(0, 101);
        if (coin <= 30)
        {
            int unit = rand.Next(0, unitsToClone.Count);
            ActionScript clone = unitsToClone[unit].GetComponent<ActionScript>();
            clone?.ToClone();
            field.GetComponent<RectTransform>().sizeDelta = new Vector2(field.GetComponent<RectTransform>().rect.width + 100,
                field.GetComponent<RectTransform>().rect.height + 100);
            textBox.text += $"Mage has cloned {unitsToClone[unit].GetComponent<Spawner>().unitName}!\n"; 
        }
    }
}
