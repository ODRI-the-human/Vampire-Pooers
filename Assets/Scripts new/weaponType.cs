using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponType : MonoBehaviour
{
    public int weaponHeld;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            weaponHeld = (int)WEAPONS.DARKARTS;
        }

        switch (weaponHeld)
        {
            case (int)WEAPONS.PISTOL:
                //bongus
                break;
            case (int)WEAPONS.SHOTGUN:
                gameObject.GetComponent<Attack>().fireTimerLength *= 2;
                gameObject.GetComponent<Attack>().noExtraShots += 4;
                gameObject.GetComponent<DealDamage>().finalDamageMult *= 0.5f;
                gameObject.GetComponent<Attack>().shotAngleCoeff = 0.7f;
                break;
            case (int)WEAPONS.DARKARTS:
                gameObject.GetComponent<Attack>().specialFireType = 3;
                break;
        }
    }
}
