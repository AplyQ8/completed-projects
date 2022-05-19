using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCamera;
    [SerializeField] Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    Vector3 defaultPos;
    public bool isDropped = false;
    public GameObject originalCard;

    private void Start()
    {
        defaultPos = GetComponent<RectTransform>().localPosition;
        Debug.Log("defPos is" + defaultPos.x.ToString() + defaultPos.y.ToString() + defaultPos.z.ToString());
    }
    private void Awake()
    {
        MainCamera = Camera.allCameras[0];
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        

        if(isDropped == false)
        {
            GetComponent<RectTransform>().localPosition = defaultPos;
            Debug.Log("defPos is" + defaultPos.x.ToString() + defaultPos.y.ToString() + defaultPos.z.ToString());
        }
        else
        {
            GameObject CardClone = Instantiate(originalCard, defaultPos, Quaternion.identity);
            CardClone.transform.position = defaultPos;
            Debug.Log("defPos is"+defaultPos.x.ToString() + defaultPos.y.ToString() + defaultPos.z.ToString());
        }
    }
}
