using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{
    // public int cellX;
    // public int cellY;
    public bool isBusy = false;
    //public int localCost;

    /*private void Start()
    {
        Debug.Log(transform.GetChild(0));
    }*/

    private void Update()
    {
        if(transform.childCount == 0)
        {
            isBusy = false;
        }
        else
        {
            isBusy = true;
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        GameObject MovedCard = eventData.pointerDrag;
        //MovedCard.GetComponent<RectTransform>().rect.width = GetComponent<RectTransform>().rect.width;
        MovedCard.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().rect.width,
            GetComponent<RectTransform>().rect.height);
        isBusy = true;
        //MovedCard.GetComponent<DeleteFromSlotScript>().enabled = true;
    }
    public void Delete()
    {
        isBusy = false;
        
        ObjectPooler op = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
        GameObject card = GetComponentInChildren<Transform>().GetChild(0).gameObject;
        card.GetComponent<DeleteFromSlotScript>().enabled = false;
        card.GetComponent<ScaleScript>().enabled = true;
        card.GetComponent<CardScr>().isDropped = false;
        Spawner spawn = card.GetComponent<Spawner>();
        money m = GameObject.Find("Money").GetComponent<money>();
        m.AmountOfMoney += spawn.Cost;
        card.GetComponent<CanvasGroup>().blocksRaycasts = true;
        op.PlaceInPool(card, spawn.unitName);
        //Debug.Log(GetComponentInChildren<Transform>().childCount);
        //GameObject.Find("Array").GetComponent<ArrayUnits>().units[cellX, cellY] = null;
    }
    

}
