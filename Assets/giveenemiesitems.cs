using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveenemiesitems : MonoBehaviour
{
    void Start()
    {
        //gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.MORESHOT);
        gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.REROLL);
    }
}
