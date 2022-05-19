using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedSpearState : IKnightState
{
    public void StartState(GameObject gameObject) { }

    public void WornHelmet(GameObject knight)
    {
        knight.GetComponent<Spawner>().Attack += 15;
        knight.GetComponent<States>().spear.SetState(new WornSpearState());
        knight.GetComponent<States>().spear.worn = true;
    }

    public void WornShield(GameObject knight) { }

    public void WornSpear(GameObject knight) { }

    public void WornHorse(GameObject knight) { }

    public void KnockedHelmet(GameObject knight) { }

    public void KnockedShield(GameObject knight) { }

    public void KnockedSpear(GameObject knight) { }

    public void KnockedHorse(GameObject knight) { }
}
