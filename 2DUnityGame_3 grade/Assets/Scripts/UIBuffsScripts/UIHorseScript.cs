using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHorseScript : MonoBehaviour
{
    private HorseBuff _hB;
    private CanvasGroup _canvas;

    private void Awake()
    {
        _hB = GetComponentInParent<HorseBuff>();
        _canvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
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
