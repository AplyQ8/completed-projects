using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelmetScript : MonoBehaviour
{
    private HelmetBuff _hB;
    private CanvasGroup _canvas;

    private void Awake()
    {
        _hB = GetComponentInParent<HelmetBuff>();
        _canvas = GetComponent<CanvasGroup>();
    }

    
    void Update()
    {
        if (_hB.worn)
            _canvas.alpha = 1f;
        else
        {
            _canvas.alpha = .0f;
        }
    }
}
