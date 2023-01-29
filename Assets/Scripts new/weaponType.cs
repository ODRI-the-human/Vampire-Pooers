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
            weaponHeld = (int)ITEMLIST.GRENADELAUNCHER;
        }

        SetWeapon();
    }

    public void SetWeapon()
    {
        switch (weaponHeld)
        {
            case (int)ITEMLIST.PISTOL:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
                    gameObject.GetComponent<Attack>().shotSpeed = 10;
                }
                break;
            case (int)ITEMLIST.GRENADELAUNCHER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.5f;
                    gameObject.GetComponent<Attack>().shotSpeed = 45;
                }
                break;
            case (int)ITEMLIST.DARKARTS:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 3;
                }
                break;
        }
    }
}
