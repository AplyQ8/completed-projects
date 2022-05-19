using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButtonScript : MonoBehaviour
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
        if (_bS.turnBackCount == 0)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0.2f;
        }
        else
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
        }
    }
}
