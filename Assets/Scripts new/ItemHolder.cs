using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public List<int> itemsHeld = new List<int>();
    public int itemGained;
    int timermf;

    void Start()
    {
        foreach (int item in itemsHeld)
        {
            itemGained = item;
            ApplyItems();
        }
    }

    void ApplyItems()
    {
        switch (itemGained)
        {
            case (int)ITEMLIST.HP25:
                gameObject.AddComponent<ItemHP25>();
                break;
            case (int)ITEMLIST.HP50:
                gameObject.AddComponent<ItemHP50>();
                break;
            case (int)ITEMLIST.DMGADDPT5:
                gameObject.AddComponent<ItemDMGADDPT5>();
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            Debug.Log("POOP! HAHA!");
            itemGained = col.gameObject.GetComponent<itemPedestal>().itemChosen;
            itemsHeld.Add(itemGained);
            ApplyItems();
        }
    }
}
