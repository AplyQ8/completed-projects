using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedoButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject battleProcess;
    [SerializeField] private GameObject turnCounter;
    private BattleScript _bS;
    private CanvasGroup _canvasGroup;
    private TurnCountScript _tCs;
    private void Awake()
    {
        _bS = battleProcess.GetComponent<BattleScript>();
        _tCs = turnCounter.GetComponent<TurnCountScript>();
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (_bS.turnBackCount == _tCs.turnCount)
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
