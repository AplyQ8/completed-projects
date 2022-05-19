using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IKnightState
{
    void StartState(GameObject gameObject);
    void WornHelmet(GameObject knight);
    void WornShield(GameObject knight);
    void WornSpear(GameObject knight);
    void WornHorse(GameObject knight);
    void KnockedHelmet(GameObject knight);
    void KnockedShield(GameObject knight);
    void KnockedSpear(GameObject knight);
    void KnockedHorse(GameObject knight);
}
