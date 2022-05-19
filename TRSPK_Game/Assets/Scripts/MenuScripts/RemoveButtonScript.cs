using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemoveButtonScript : MonoBehaviour
{
    public GameObject slot;
    public TMP_Text text;
    SlotScript slotScr;

    private void Start()
    {
        slotScr = slot.GetComponent<SlotScript>();
    }
    private void Update()
    {
        if(slotScr.isBusy)
        {
            GetComponent<CanvasGroup>().alpha = 1f;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 0f;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    /*public void Delete()
    {
        slotScr.Delete();
    }*/
}
