using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedHelmetState: IKnightState
{
    public void StartState(GameObject gameObject) { }

    public void WornHelmet(GameObject knight)
    {
        knight.GetComponent<Spawner>()._maxDef += 15;
        knight.GetComponent<Spawner>().Defence += 15;
        knight.GetComponent<States>().helmet.SetState(new WornHelmetState());
        knight.GetComponent<States>().helmet.worn = true;
    }

    public void WornShield(GameObject knight) { }

    public void WornSpear(GameObject knight) { }

    public void WornHorse(GameObject knight) { }

    public void KnockedHelmet(GameObject knight) { }

    public void KnockedShield(GameObject knight) { }

    public void KnockedSpear(GameObject knight) { }

    public void KnockedHorse(GameObject knight) { }
}
