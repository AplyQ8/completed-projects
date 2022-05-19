using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseBuff : MonoBehaviour
{
    private IKnightState _knightState = new KnightStartState();
    public bool worn;
    public void SetState(IKnightState knightState) => this._knightState = knightState;

    public void Wear() => _knightState.WornHorse(gameObject);
    public void Knock() => _knightState.KnockedHorse(gameObject);
}
