using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject battleProcess;
    private BattleScript _bS;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _bS = battleProcess.GetComponent<BattleScript>();
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (_bS.fightIsOver)
        {
            _canvasGroup.alpha = 0.2f;
            _canvasGroup.interactable = false;
        }
    }
}
