using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShieldScript : MonoBehaviour
{
    private ShieldBuff _sB;
    private CanvasGroup _canvas;

    private void Awake()
    {
        _sB = GetComponentInParent<ShieldBuff>();
        _canvas = GetComponent<CanvasGroup>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (_sB.worn)
            _canvas.alpha = 1f;
        else
        {
            _canvas.alpha = .0f;
        }
    }
}
