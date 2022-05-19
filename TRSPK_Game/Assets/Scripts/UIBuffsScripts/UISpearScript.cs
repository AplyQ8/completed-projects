using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpearScript : MonoBehaviour
{
    private SpearBuff _sB;
    private CanvasGroup _canvas;

    private void Awake()
    {
        _sB = GetComponentInParent<SpearBuff>();
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
