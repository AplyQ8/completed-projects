using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceBarScript : MonoBehaviour
{
    public Slider slider;
    private CardScr _cardScr;
    private CanvasGroup _canvas;
    //public Gradient gradient;
    private void Awake()
    {
        _cardScr = GetComponentInParent<CardScr>();
        _canvas = GetComponent<CanvasGroup>();
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        //gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    private void Update()
    {
        if (_cardScr.isDropped)
            _canvas.alpha = 1f;
    }
}
