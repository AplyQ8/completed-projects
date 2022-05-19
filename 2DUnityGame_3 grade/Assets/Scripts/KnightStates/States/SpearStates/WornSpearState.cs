using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornSpearState : IKnightState
{
    public void StartState(GameObject gameObject) { }

    public void WornHelmet(GameObject knight) { }

    public void WornShield(GameObject knight) { }

    public void WornSpear(GameObject knight) { }

    public void WornHorse(GameObject knight) { }

    public void KnockedHelmet(GameObject knight) { }

    public void KnockedShield(GameObject knight) { }

    public void KnockedSpear(GameObject knight)
    {
        knight.GetComponent<Spawner>().Attack -= 15;
        knight.GetComponent<States>().spear.SetState(new KnockedSpearState());
        knight.GetComponent<States>().spear.worn = false;
    }

    public void KnockedHorse(GameObject knight) { }
}
