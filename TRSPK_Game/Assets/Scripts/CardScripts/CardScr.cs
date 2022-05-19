using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera MainCamera;
    private Vector3 offset;
    public Transform DefaultParent, DefaultTmpCardParent;
    public Transform parent;
    [SerializeField] Canvas canvas;
    CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public bool isDropped = false;
    public Vector3 defaultPos;
    

    private GameObject TempCardGO;
    void Awake()
    {
        MainCamera = Camera.main;
        TempCardGO = GameObject.Find("TempCardGO");
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        defaultPos = GetComponent<RectTransform>().localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = DefaultTmpCardParent = transform.parent;

        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if (TempCardGO.transform.parent != DefaultTmpCardParent)
        {
            TempCardGO.transform.SetParent(DefaultTmpCardParent);
        }

        CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        
        transform.SetParent(DefaultParent);

        
        // if(isDropped == true)
        // {
        //     GetComponent<CanvasGroup>().blocksRaycasts = false;
        //     
        // }
        // else
        // {
        //     GetComponent<CanvasGroup>().blocksRaycasts = true;
        // }
        if (isDropped)
        {
            GetComponent<DeleteFromSlotScript>().enabled = true;
            GetComponent<ScaleScript>().enabled = false;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());

        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(5000, 0);
        //Debug.Log("Called From CardScr");
    }

    void CheckPosition()
    {
        var newIndex = DefaultTmpCardParent.childCount;
        for (int i = 0; i < DefaultTmpCardParent.childCount; i++)
        {
            if (transform.position.x < DefaultTmpCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                {
                    newIndex--;
                }

                break;
            }
        }

        TempCardGO.transform.SetSiblingIndex(newIndex);
        
    }
}
