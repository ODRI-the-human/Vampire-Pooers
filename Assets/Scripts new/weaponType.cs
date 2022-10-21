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
            weaponHeld = (int)WEAPONS.PISTOL;
        }

        SetWeapon();
    }

    public void SetWeapon()
    {
        switch (weaponHeld)
        {
            case (int)WEAPONS.PISTOL:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                }
                break;
            case (int)WEAPONS.DARKARTS:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 3;
                }
                break;
        }
    }
}
