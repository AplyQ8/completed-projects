using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    public GameObject playerField;
    public GameObject enemyField;
    public GameObject staticPlayerField;
    public GameObject staticEnemyField;
    public GameObject coinObject;
    System.Random rand = new System.Random();
    public int coin;
    public List<Memento> eMementos;
    public List<Memento> pMementos;
    public int turnBackCount;
    public bool isDead;
    public GameObject pFieldParent;
    public GameObject eFieldParent;
    private bool turnChanged = false;
    public bool fightIsOver = false;
    private int actionTurn = 0;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject nextTurnButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private TMP_Text playerMessage;
    [SerializeField] private TMP_Text enemyMessage;


    public void Awake()
    {
        eMementos = new List<Memento>();
        pMementos = new List<Memento>();
        playerField = GameObject.Find("FieldScroll").transform.GetChild(0).gameObject;
        playerField.GetComponent<PlayerField>().field = GameObject.Find("FieldScroll").transform.GetChild(0).gameObject;
        enemyField = GameObject.Find("FieldScroll(Clone)").transform.GetChild(0).gameObject;
        enemyField.GetComponent<PlayerField>().field = GameObject.Find("FieldScroll(Clone)").transform.GetChild(0).gameObject;
        staticPlayerField = GameObject.Find("FieldScroll").transform.GetChild(0).gameObject;
        staticEnemyField = GameObject.Find("FieldScroll(Clone)").transform.GetChild(0).gameObject;
        coin = coinObject.GetComponent<Coin_Flip>().coin;
        pFieldParent = GameObject.Find("PlayerSide");
        eFieldParent = GameObject.Find("EnemySide");
        

    }
    
    public void Ready()
    {
        SaveToLists();
    }

    public void Fight()
    {
        
        if(turnChanged)
        {
            pMementos[turnBackCount]._field.SetActive(false);
            eMementos[turnBackCount]._field.SetActive(false);
            staticPlayerField.SetActive(true);
            staticEnemyField.SetActive(true);
            playerField = staticPlayerField;
            enemyField = staticEnemyField;
            pFieldParent.GetComponent<ScrollRect>().content = playerField.GetComponent<RectTransform>();
            eFieldParent.GetComponent<ScrollRect>().content = enemyField.GetComponent<RectTransform>();
            turnChanged = false;
        }
        isDead = false;
        int playerUnit = CheckPlayerUnits(0, playerField);
        if (playerUnit == -1)
        {
            text.text = "Defeat...Try again?";
            fightIsOver = true;
            nextTurnButton.GetComponent<CanvasGroup>().alpha = 0.3f;
            nextTurnButton.GetComponent<CanvasGroup>().interactable = false;
            backButton.SetActive(true);
            return;
        }
        int enemyUnit = CheckPlayerUnits(0, enemyField);
        if (enemyUnit == -1)
        {
            text.text = "Victory!";
            fightIsOver = true;
            nextTurnButton.GetComponent<CanvasGroup>().alpha = 0.3f;
            nextTurnButton.GetComponent<CanvasGroup>().interactable = false;
            backButton.SetActive(true);
            return;
        }
        while(!isDead)
        {
            if(coin%2 == 1)
            {
                Duel(playerField.transform.GetChild(playerUnit).GetChild(0).gameObject, enemyField.transform.GetChild(enemyUnit).GetChild(0).gameObject);
                if(enemyField.transform.GetChild(enemyUnit).GetChild(0).GetComponent<Spawner>().HP <= 0)
                {
                    isDead = true;
                    playerMessage.text += $"{actionTurn}.\n";
                    LoopForCallback(playerUnit + 1, playerField.transform.childCount, playerField, enemyField, playerMessage);
                    actionTurn++;
                }
            }
            else
            {
                Duel(enemyField.transform.GetChild(enemyUnit).GetChild(0).gameObject, playerField.transform.GetChild(playerUnit).GetChild(0).gameObject);
                if(playerField.transform.GetChild(playerUnit).GetChild(0).GetComponent<Spawner>().HP <= 0)
                {
                    isDead = true;
                    enemyMessage.text += $"{actionTurn}.\n";
                    LoopForCallback(enemyUnit + 1, enemyField.transform.childCount, enemyField, playerField, enemyMessage);
                    actionTurn++;
                }
            }
            coin++;
        }
        GameObject.Find("TurnCounter").GetComponent<TurnCountScript>().turnCount++;
        turnBackCount = GameObject.Find("TurnCounter").GetComponent<TurnCountScript>().turnCount;
        SaveToLists();
    }

    public void SaveToLists()
    {
        var tempObj = Instantiate(playerField, playerField.transform.position, playerField.transform.rotation, playerField.transform.parent);
        playerField.GetComponent<PlayerField>().field = tempObj;
        playerField.GetComponent<PlayerField>().SaveTo(pMementos);
        playerField.GetComponent<PlayerField>().field.SetActive(false);
        
        var tempObj1 = Instantiate(enemyField, enemyField.transform.position, enemyField.transform.rotation, enemyField.transform.parent);
        enemyField.GetComponent<PlayerField>().field = tempObj1;
        enemyField.GetComponent<PlayerField>().SaveTo(eMementos);
        enemyField.GetComponent<PlayerField>().field.SetActive(false);
    }
    public void Undo()
    {
        turnChanged = true;
        turnBackCount -= 1;
        playerField.GetComponent<PlayerField>().field = playerField;
        playerField.GetComponent<PlayerField>().RestoreState(pMementos[turnBackCount],GameObject.Find("FieldScroll"));
        playerField = pMementos[turnBackCount]._field;
        enemyField.GetComponent<PlayerField>().field = enemyField;
        enemyField.GetComponent<PlayerField>().RestoreState(eMementos[turnBackCount],GameObject.Find("FieldScroll(Clone)"));
        enemyField = eMementos[turnBackCount]._field;
    }
    public void Redo()
    {
        turnBackCount += 1;
        playerField.GetComponent<PlayerField>().field = playerField;
        playerField.GetComponent<PlayerField>().RestoreState(pMementos[turnBackCount], GameObject.Find("FieldScroll"));
        playerField = pMementos[turnBackCount]._field;
        enemyField.GetComponent<PlayerField>().field = enemyField;
        enemyField.GetComponent<PlayerField>().RestoreState(eMementos[turnBackCount], GameObject.Find("FieldScroll(Clone)"));
        enemyField = eMementos[turnBackCount]._field;
    }




    private int CheckPlayerUnits(int playerUnit, GameObject field)
    {
        for(int i = playerUnit; i < field.transform.childCount; i++)
        {
            if (field.transform.GetChild(i).childCount > 0)
            {
                if(field.transform.GetChild(i).GetChild(0).gameObject.activeSelf)
                    return i;
            }
        }
        return -1;
    }
    private void Duel(GameObject playerUnit, GameObject enemyUnit)
    {
        
        ActionScript actionFirst = enemyUnit.GetComponent<ActionScript>();
        actionFirst?.TakeDamage(playerUnit.GetComponent<Spawner>().Attack);
    }
    private void CallSpecAction(GameObject unit, GameObject field, GameObject enemyField, int index, TMP_Text textBox)
    {
        ActionScript Action = unit.GetComponent<ActionScript>();
        Action?.Ultimate(field, enemyField, index, textBox);
    }
    private void LoopForCallback(int start, int end, GameObject field, GameObject enemyField, TMP_Text textBox)
    {
        for(int i = start; i < end; i ++)
        {
            if(field.transform.GetChild(i).childCount > 0)
            {
                CallSpecAction(field.transform.GetChild(i).GetChild(0).gameObject, field, enemyField, i, textBox);
            }
        }
    }

    public void FightTillTheEnd()
    {
        while (!fightIsOver)
        {
            Fight();
        }
    }
}
