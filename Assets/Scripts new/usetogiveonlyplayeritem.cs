using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usetogiveonlyplayeritem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGMLT2);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGMLT2);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGMLT2);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGMLT2);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGADDPT5);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGADDPT5);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGADDPT5);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGADDPT5);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGADDPT5);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.DMGADDPT5);
    }
}
