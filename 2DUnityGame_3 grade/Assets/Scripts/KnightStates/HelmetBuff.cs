using System;
using UnityEngine;


public class HelmetBuff : MonoBehaviour
{
    private IKnightState _knightState = new KnightStartState();
    public bool worn;
    public void SetState(IKnightState knightState) => this._knightState = knightState;

    public void Wear() => _knightState.WornHelmet(gameObject);
    public void Knock() => _knightState.KnockedHelmet(gameObject);
    
}