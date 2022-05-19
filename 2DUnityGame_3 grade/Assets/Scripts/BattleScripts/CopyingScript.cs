using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyingScript : MonoBehaviour
{
    public string pField;
    public GameObject ActiveSide;
    [SerializeField] private GameObject _battleProcess;
    private void Awake()
    {
        GameObject playerField = GameObject.Find(pField);
        //GameObject playerSide = GameObject.Find("PlayerSide");
        if(playerField)
        {
            playerField.GetComponent<Transform>().transform.SetParent(ActiveSide.GetComponent<Transform>());
            playerField.GetComponent<Transform>().transform.position = ActiveSide.GetComponent<Transform>().transform.position;
            playerField.GetComponent<RectTransform>().sizeDelta = new Vector2(
                ActiveSide.GetComponent<RectTransform>().rect.width,
                ActiveSide.GetComponent<RectTransform>().rect.height);
            ActiveSide.GetComponent<ScrollRect>().content = playerField.GetComponent<RectTransform>();
            //playerField.GetComponent<RectTransform>().anchoredPosition = 

        }
        else
        {
            Debug.LogWarning("Object have not found");
        }

        _battleProcess.SetActive(true);
    }
}
