using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerField : MonoBehaviour
{
    
    public GameObject field;

    public void SaveTo(IList<Memento> collection)
    {
        collection.Add(new Memento(field));
        Debug.Log("Saved! Count: " + collection.Count);

    }
    public void RestoreState(Memento state, GameObject parent) // передавать текущие field'ы, присваивать State field'ам parent& position переданных
    {
        field.SetActive(false);
        //_field = state._field;
        state._field.SetActive(true);
        parent.GetComponent<ScrollRect>().content = state._field.GetComponent<RectTransform>();
    }
    
}
