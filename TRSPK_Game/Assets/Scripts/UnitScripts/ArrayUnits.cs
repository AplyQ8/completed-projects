using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrayUnits: MonoBehaviour
{
    [SerializeField]
    public IUnit[,] units; //new IUnit[3, 3];

    private void Start()
    {
        int columns = GameObject.Find("Field").GetComponent<GridLayoutGroup>().constraintCount;
        units = new IUnit[GameObject.Find("Field").transform.childCount / columns, columns];
        Debug.Log(GameObject.Find("Field").transform.childCount / columns + "- rows; " + columns + " -columns");
    }




}
