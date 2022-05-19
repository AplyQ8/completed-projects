using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteFromSlotScript: MonoBehaviour, IPointerClickHandler
{
    private SlotScript _slotScript;
    private void Awake()
    {
        enabled = false;
        //_slotScript = GetComponent<SlotScript>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _slotScript = GetComponentInParent<SlotScript>();
        _slotScript.Delete();
    }
}