using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usetogiveonlyplayeritem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.PISTOL);
        //gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.MARCEL);
        //gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.LUCKIER);
        //gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.ORBITAL2);
    }
}
