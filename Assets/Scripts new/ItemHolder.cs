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
            case (int)ITEMLIST.DMGMLT2:
                gameObject.AddComponent<ItemDMGMLT2>();
                break;
            case (int)ITEMLIST.FIRERATE:
                gameObject.AddComponent<ItemFIRERATE>();
                break;
            case (int)ITEMLIST.SOY:
                gameObject.AddComponent<ItemSOY>();
                break;
            case (int)ITEMLIST.HOMING:
                if (gameObject.GetComponent<ItemHOMING>() == null)
                {
                    gameObject.AddComponent<ItemHOMING>();
                }
                else
                {
                    gameObject.GetComponent<ItemHOMING>().instances++;
                }
                break;
            case (int)ITEMLIST.ATG:
                if (gameObject.GetComponent<ItemATG>() == null)
                {
                    gameObject.AddComponent<ItemATG>();
                }
                else
                {
                    gameObject.GetComponent<ItemATG>().instances++;
                }
                break;
            case (int)ITEMLIST.MORESHOT:
                gameObject.AddComponent<ItemMORESHOT>();
                break;
            case (int)ITEMLIST.WAPANT:
                if (gameObject.GetComponent<ItemWAPANT>() == null)
                {
                    gameObject.AddComponent<ItemWAPANT>();
                }
                else
                {
                    gameObject.GetComponent<ItemWAPANT>().wapantTimerLength /= 1.2f;
                    gameObject.GetComponent<ItemWAPANT>().instances++;
                }
                break;
            case (int)ITEMLIST.HOLYMANTIS:
                if (gameObject.GetComponent<ItemHOLYMANTIS>() == null)
                {
                    gameObject.AddComponent<ItemHOLYMANTIS>();
                }
                else
                {
                    gameObject.GetComponent<ItemHOLYMANTIS>().instances++;
                    gameObject.GetComponent<ItemHOLYMANTIS>().maxTimesHit++;
                    gameObject.GetComponent<ItemHOLYMANTIS>().timesHit = gameObject.GetComponent<ItemHOLYMANTIS>().maxTimesHit;
                }
                break;
            case (int)ITEMLIST.CONVERTER:
                if (gameObject.GetComponent<ItemCONVERTER>() == null)
                {
                    gameObject.AddComponent<ItemCONVERTER>();
                }
                else
                {
                    gameObject.GetComponent<ItemCONVERTER>().instances++;
                }
                break;
            case (int)ITEMLIST.EASIERTIMES:
                if (gameObject.GetComponent<ItemEASIERTIMES>() == null)
                {
                    gameObject.AddComponent<ItemEASIERTIMES>();
                }
                else
                {
                    gameObject.GetComponent<ItemEASIERTIMES>().instances++;
                }
                break;
            case (int)ITEMLIST.STOPWATCH:
                if (gameObject.GetComponent<ItemSTOPWATCH>() == null)
                {
                    gameObject.AddComponent<ItemSTOPWATCH>();
                }
                else
                {
                    gameObject.GetComponent<ItemSTOPWATCH>().instances++;
                }
                break;
            case (int)ITEMLIST.BOUNCY:
                if (gameObject.GetComponent<ItemBOUNCY>() == null)
                {
                    gameObject.AddComponent<ItemBOUNCY>();
                }
                else
                {
                    gameObject.GetComponent<ItemBOUNCY>().instances++;
                }
                break;
            case (int)ITEMLIST.FOURDIRMARTY:
                if (gameObject.GetComponent<ItemFOURDIRMARTY>() == null)
                {
                    gameObject.AddComponent<ItemFOURDIRMARTY>();
                }
                else
                {
                    gameObject.GetComponent<ItemFOURDIRMARTY>().instances++;
                }
                break;
            case (int)ITEMLIST.PIERCING:
                if (gameObject.GetComponent<ItemPIERCING>() == null)
                {
                    gameObject.AddComponent<ItemPIERCING>();
                }
                else
                {
                    gameObject.GetComponent<ItemPIERCING>().instances++;
                }
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
