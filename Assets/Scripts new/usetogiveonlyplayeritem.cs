using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usetogiveonlyplayeritem : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.PISTOL;
        //gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.FAMILIAR);
    }
}
