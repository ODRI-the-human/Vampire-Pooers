using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBAT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<weaponType>() != null)
        {
            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.BAT;
            gameObject.GetComponent<weaponType>().SetWeapon();
        }
    }

    void Undo()
    {
        Destroy(this);
    }
}
