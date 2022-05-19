using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento
{
   public GameObject _field;
        
    public Memento(GameObject _pField)
    {
        _field = _pField;
    }

    public GameObject _Field
    {
        get => this._field;
    }
    
}
