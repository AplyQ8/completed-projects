using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuff : MonoBehaviour
{
    private IKnightState _knightState = new KnightStartState();
    public bool worn;
    public void SetState(IKnightState knightState) => this._knightState = knightState;

    public void Wear() => _knightState.WornShield(gameObject);
    public void Knock() => _knightState.KnockedShield(gameObject);
}
