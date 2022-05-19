using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedHorseState : IKnightState
{
    public void StartState(GameObject gameObject) { }

    public void WornHelmet(GameObject knight)
    {
        knight.GetComponent<Spawner>()._maxHealth += 15;
        knight.GetComponent<Spawner>().HP += 15;
        knight.GetComponent<States>().horse.SetState(new WornHorseState());
        knight.GetComponent<States>().horse.worn = true;
    }

    public void WornShield(GameObject knight) { }

    public void WornSpear(GameObject knight) { }

    public void WornHorse(GameObject knight) { }

    public void KnockedHelmet(GameObject knight) { }

    public void KnockedShield(GameObject knight) { }

    public void KnockedSpear(GameObject knight) { }

    public void KnockedHorse(GameObject knight) { }
}
