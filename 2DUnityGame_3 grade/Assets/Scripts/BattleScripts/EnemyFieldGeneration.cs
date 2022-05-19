using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class EnemyFieldGeneration : MonoBehaviour
{
    private GameObject enemy;
    private GameObject possibleCards;
    private int money;
    System.Random rand = new System.Random();
    private bool isPossible = true;
    private List<GameObject> Slots = new List<GameObject>();
    private void Start()
    {
        GameObject field = GameObject.Find("FieldScroll(Clone)");
        //enemy = GameObject.Find("FieldScroll(Clone)")/*.transform.GetChild(0).gameObject*/;
        enemy = field.transform.GetChild(0).gameObject;
        possibleCards = GameObject.Find("Hand");
        money = GameObject.Find("Money").GetComponent<money>().origMoney;
        FillingList();
        //Debug.Log(Slots.Count);

        isPossible = true;

        while(isPossible && Slots.Count!=0)
        {
            
            
            GameObject card = possibleCards.transform.GetChild(rand.Next(0, possibleCards.transform.childCount - 1)).gameObject;
            int availableSlot = rand.Next(0, Slots.Count - 1);
            //Debug.Log(availableSlot);
            GameObject slot = Slots[availableSlot];
            //Debug.Log(slot.GetComponent<SlotScript>().cellX +" "+ slot.GetComponent<SlotScript>().cellY + "- slot");
            if(!slot.GetComponent<SlotScript>().isBusy)
            {
                
                if(card.GetComponent<Spawner>().Cost <= money)
                {
                    //назначаем родителя
                    card.transform.SetParent(slot.transform);
                    card.transform.position = slot.transform.position;
                    card.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    card.GetComponent<RectTransform>().sizeDelta = new Vector2(slot.GetComponent<RectTransform>().rect.width,
                        slot.GetComponent<RectTransform>().rect.height);
                    card.GetComponent<CardScr>().isDropped = true;


                    //Debug.Log(card.GetComponent<Spawner>().Cost + card.GetComponent<Spawner>().unitName + "- name of unit");
                    //вытаскиваем из пуля
                    Spawner spawn = card.GetComponent<Spawner>();
                    spawn.Spawn();
                    //вычитаем деньги
                    money -= card.GetComponent<Spawner>().Cost;
                    //Убираем из списка ячейку
                    Slots.RemoveAt(availableSlot);
                    //Debug.Log(card.GetComponent<Spawner>().unitName);
                }
                else
                {
                    //Проверяем возможно ли еще перетащить карты(CheckPossibility()
                    if(CheckPossibility())
                    {
                        //да - Функция из доступных объектов - рандомим & Убираем из списка ячейку
                        List<GameObject> cards = AvailableUnits();
                        card = cards[rand.Next(0, cards.Count - 1)];
                        card.transform.SetParent(slot.transform);
                        card.transform.position = slot.transform.position;
                        card.GetComponent<CanvasGroup>().blocksRaycasts = false;
                        card.GetComponent<RectTransform>().sizeDelta = new Vector2(slot.GetComponent<RectTransform>().rect.width,
                            slot.GetComponent<RectTransform>().rect.height);
                        card.GetComponent<CardScr>().isDropped = true;


                        //Debug.Log(card.GetComponent<Spawner>().Cost + card.GetComponent<Spawner>().unitName + "- name of unit");

                        Spawner spawn = card.GetComponent<Spawner>();
                        spawn.Spawn();
                        money -= card.GetComponent<Spawner>().Cost;
                        Slots.RemoveAt(availableSlot);
                    }
                    else
                    {
                        //нет - isPossible = false
                        isPossible = false;
                    }


                }
            }
        }
        

    }
    private void FillingList()
    {
        for(int i = 0; i < enemy.transform.childCount; i++)
        {
            Slots.Add(enemy.transform.GetChild(i).gameObject);
        }
    }

    private bool CheckPossibility()
    {
        for(int i = 0; i < possibleCards.transform.childCount; i++)
        {
            if (possibleCards.transform.GetChild(i).GetComponent<Spawner>().Cost <= money)
            {
                //Debug.Log($"{possibleCards.transform.GetChild(i).GetComponent<Spawner>().Cost} - cost of {possibleCards.transform.GetChild(i).GetComponent<Spawner>().unitName}");
                return true;
            }
                
        }
        return false;

    }
    private List<GameObject> AvailableUnits()
    {
        List<GameObject> cards = new List<GameObject>();
        for (int i = 0; i < possibleCards.transform.childCount; i++)
        {
            if (possibleCards.transform.GetChild(i).GetComponent<Spawner>().Cost <= money)
            {
                cards.Add(possibleCards.transform.GetChild(i).gameObject);
            }
        }
        //Debug.Log("Available units: ");
        for(int i = 0; i < cards.Count; i++)
        {
            //Debug.Log(cards[i].GetComponent<Spawner>().unitName);
        }
        return cards;
    }
}
