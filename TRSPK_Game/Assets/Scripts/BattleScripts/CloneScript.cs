using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour
{
    private void Start()
    {
        GameObject strategy = GameObject.Find("Strategy");
        Context context = strategy.GetComponent<Context>();
        context.Execute();
        GameObject newField = Instantiate(GameObject.Find("FieldScroll"));
        
        //newField.SetActive(false);
        newField.transform.SetParent(GameObject.Find("Canvas").transform);
        newField.transform.position = new Vector2(1000, 1000);
    }
}
