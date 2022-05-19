using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornShieldState : IKnightState
{
    public void StartState(GameObject gameObject) { }

    public void WornHelmet(GameObject knight) { }

    public void WornShield(GameObject knight) { }

    public void WornSpear(GameObject knight) { }

    public void WornHorse(GameObject knight) { }

    public void KnockedHelmet(GameObject knight) { }

    public void KnockedShield(GameObject knight)
    {
        knight.GetComponent<Spawner>()._maxDef -= 15;
        knight.GetComponent<Spawner>().Defence -= 15;
        knight.GetComponent<States>().shield.SetState(new KnockedShieldState());
        knight.GetComponent<States>().shield.worn = false;
    }

    public void KnockedSpear(GameObject knight) { }

    public void KnockedHorse(GameObject knight) { }
}
