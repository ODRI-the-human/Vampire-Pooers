using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPISTOL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.PISTOL;
        gameObject.GetComponent<weaponType>().SetWeapon();
    }

    void Undo()
    {
        Destroy(this);
    }
}
