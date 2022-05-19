using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightStartState : IKnightState
{
    public void StartState(GameObject gameObject) { }

    public void WornHelmet(GameObject knight)
    {
        knight.GetComponent<Spawner>()._maxDef += 15;
        knight.GetComponent<Spawner>().Defence += 15;
        knight.GetComponent<States>().helmet.SetState(new WornHelmetState());
        knight.GetComponent<States>().helmet.worn = true;
    }

    public void WornShield(GameObject knight)
    {
        knight.GetComponent<Spawner>()._maxDef += 15;
        knight.GetComponent<Spawner>().Defence += 15;
        knight.GetComponent<States>().shield.SetState(new WornShieldState());
        knight.GetComponent<States>().shield.worn = true;
    }

    public void WornSpear(GameObject knight)
    {
        knight.GetComponent<Spawner>().Attack += 15;
        knight.GetComponent<States>().spear.SetState(new WornSpearState());
        knight.GetComponent<States>().spear.worn = true;
    }

    public void WornHorse(GameObject knight)
    {
        knight.GetComponent<Spawner>()._maxHealth += 15;
        knight.GetComponent<Spawner>().HP += 15;
        knight.GetComponent<States>().horse.SetState(new WornHorseState());
        knight.GetComponent<States>().horse.worn = true;
    }

    public void KnockedHelmet(GameObject knight) { }

    public void KnockedShield(GameObject knight) { }

    public void KnockedSpear(GameObject knight) { }

    public void KnockedHorse(GameObject knight) { }
}
