using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usetogiveonlyplayeritem : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<ItemHolder>().weaponHeld = (int)ITEMLIST.PISTOL;
        //gameObject.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.FAMILIAR);
        // For giving the player some number of random items.
        //for (int i = 0; i < 50; i++)
        //{
        //    int item = Random.Range(0, (int)ITEMLIST.PISTOL);
        //    while (item == (int)ITEMLIST.REROLL)
        //    {
        //        item = Random.Range(0, (int)ITEMLIST.PISTOL);
        //    }
        //    gameObject.GetComponent<ItemHolder>().itemsHeld.Add(item);
        //}
    }
}
