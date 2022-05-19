using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingInvisible : MonoBehaviour
{
    private CardScr _cardScr;
    private CanvasGroup _canvas;

    private void Awake()
    {
        _cardScr = GetComponentInParent<CardScr>();
        _canvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_cardScr.isDropped)
        {
            _canvas.alpha = .0f;
        }
    }
}
