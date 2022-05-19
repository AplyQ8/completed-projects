using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornHelmetState : IKnightState
{
    public void StartState(GameObject gameObject) { }

    public void WornHelmet(GameObject knight)
    {
        Debug.Log("Already worn!");
    }

    public void WornShield(GameObject knight) { }

    public void WornSpear(GameObject knight) { }

    public void WornHorse(GameObject knight) { }

    public void KnockedHelmet(GameObject knight)
    {
        knight.GetComponent<Spawner>()._maxDef -= 15;
        knight.GetComponent<Spawner>().Defence -= 15;
        knight.GetComponent<States>().helmet.SetState(new KnockedHelmetState());
        knight.GetComponent<States>().helmet.worn = false;
    }

    public void KnockedShield(GameObject knight) { }

    public void KnockedSpear(GameObject knight) { }

    public void KnockedHorse(GameObject knight) { }
}
