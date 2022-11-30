using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public List<int> itemsHeld = new List<int>();
    public GameObject master;
    bool doApply = true;
    public int itemGained;
    public int noToGive = 1;
    int timermf;

    void Start()
    {
        master = GameObject.Find("bigFuckingMasterObject");
        ApplyAll();
    }

    public void ApplyAll()
    {
        foreach (int item in itemsHeld)
        {
            itemGained = item;
            master.GetComponent<ItemDescriptions>().itemChosen = itemGained;
            master.GetComponent<ItemDescriptions>().getItemDescription();
            doApply = master.GetComponent<ItemDescriptions>().applyToBullets;

            if (gameObject.tag == "Hostile" || gameObject.tag == "Player")
            {
                doApply = true;
            }

            if (doApply)
            {
                ApplyItems();
            }
            doApply = true;
        }
    }

    public void ApplyItems()
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
            case (int)ITEMLIST.CREEP:
                if (gameObject.GetComponent<ItemCREEP>() == null)
                {
                    gameObject.AddComponent<ItemCREEP>();
                }
                else
                {
                    gameObject.GetComponent<ItemCREEP>().instances++;
                }
                break;
            case (int)ITEMLIST.DODGESPLOSION:
                if (gameObject.GetComponent<ItemDODGESPLOSION>() == null)
                {
                    gameObject.AddComponent<ItemDODGESPLOSION>();
                }
                else
                {
                    gameObject.GetComponent<ItemDODGESPLOSION>().instances++;
                }
                break;
            case (int)ITEMLIST.BETTERDODGE:
                gameObject.AddComponent<ItemBETTERDODGE>();
                break;
            case (int)ITEMLIST.ORBITAL1:
                if (gameObject.GetComponent<ItemORBITAL1>() == null)
                {
                    gameObject.AddComponent<ItemORBITAL1>();
                }
                else
                {
                    gameObject.GetComponent<ItemORBITAL1>().instances++;
                }
                break;
            case (int)ITEMLIST.ORBITAL2:
                if (gameObject.GetComponent<ItemORBITAL2>() == null)
                {
                    gameObject.AddComponent<ItemORBITAL2>();
                }
                else
                {
                    gameObject.GetComponent<ItemORBITAL2>().instances++;
                }
                break;
            case (int)ITEMLIST.SPLIT:
                if (gameObject.GetComponent<ItemSPLIT>() == null)
                {
                    gameObject.AddComponent<ItemSPLIT>();
                }
                else
                {
                    gameObject.GetComponent<ItemSPLIT>().instances++;
                }
                break;
            case (int)ITEMLIST.CONTACT:
                if (gameObject.GetComponent<ItemCONTACT>() == null)
                {
                    gameObject.AddComponent<ItemCONTACT>();
                }
                else
                {
                    gameObject.GetComponent<ItemCONTACT>().instances++;
                }
                break;
            case (int)ITEMLIST.BLEED:
                if (gameObject.GetComponent<ItemBLEED>() == null)
                {
                    gameObject.AddComponent<ItemBLEED>();
                }
                else
                {
                    gameObject.GetComponent<ItemBLEED>().instances++;
                }
                break;
            case (int)ITEMLIST.POISONSPLOSM:
                if (gameObject.GetComponent<ItemPOISONSPLOSM>() == null)
                {
                    gameObject.AddComponent<ItemPOISONSPLOSM>();
                }
                else
                {
                    gameObject.GetComponent<ItemPOISONSPLOSM>().instances++;
                }
                break;
            case (int)ITEMLIST.ELECTRIC:
                if (gameObject.GetComponent<ItemELECTRIC>() == null)
                {
                    gameObject.AddComponent<ItemELECTRIC>();
                }
                else
                {
                    gameObject.GetComponent<ItemELECTRIC>().instances++;
                }
                break;
            case (int)ITEMLIST.BERSERK:
                if (gameObject.GetComponent<ItemBERSERK>() == null)
                {
                    gameObject.AddComponent<ItemBERSERK>();
                }
                else
                {
                    gameObject.GetComponent<ItemBERSERK>().instances++;
                }
                break;
            case (int)ITEMLIST.HEALMLT:
                gameObject.AddComponent<ItemHEALMLT>();
                break;
            case (int)ITEMLIST.PERFECTHEAL:
                if (gameObject.GetComponent<ItemPERFECTHEAL>() == null)
                {
                    gameObject.AddComponent<ItemPERFECTHEAL>();
                }
                else
                {
                    gameObject.GetComponent<ItemPERFECTHEAL>().instances++;
                }
                break;
            case (int)ITEMLIST.REROLL:
                gameObject.AddComponent<ItemREROLL>();
                break;
            case (int)ITEMLIST.BRICK:
                if (gameObject.GetComponent<ItemBRICK>() == null)
                {
                    gameObject.AddComponent<ItemBRICK>();
                }
                else
                {
                    gameObject.GetComponent<ItemBRICK>().instances++;
                }
                break;
            case (int)ITEMLIST.BETTERLEVEL:
                gameObject.AddComponent<ItemBETTERLEVEL>();
                break;
            case (int)ITEMLIST.EXTRAITEMLEVEL:
                if (gameObject.GetComponent<ItemEXTRAITEMLEVEL>() == null)
                {
                    gameObject.AddComponent<ItemEXTRAITEMLEVEL>();
                }
                else
                {
                    gameObject.GetComponent<ItemEXTRAITEMLEVEL>().instances++;
                }
                break;
            case (int)ITEMLIST.MORELEVELSTATS:
                if (gameObject.GetComponent<ItemMORELEVELSTATS>() == null)
                {
                    gameObject.AddComponent<ItemMORELEVELSTATS>();
                }
                else
                {
                    gameObject.GetComponent<ItemMORELEVELSTATS>().instances++;
                }
                break;
            case (int)ITEMLIST.HEALTHXP:
                if (gameObject.GetComponent<ItemHEALTHXP>() == null)
                {
                    gameObject.AddComponent<ItemHEALTHXP>();
                }
                else
                {
                    gameObject.GetComponent<ItemHEALTHXP>().instances++;
                }
                break;
            case (int)ITEMLIST.LEVELHEAL:
                gameObject.AddComponent<ItemLEVELHEAL>();
                break;
            case (int)ITEMLIST.DAGGERTHROW:
                if (gameObject.GetComponent<ItemDAGGERTHROW>() == null)
                {
                    gameObject.AddComponent<ItemDAGGERTHROW>();
                }
                else
                {
                    gameObject.GetComponent<ItemDAGGERTHROW>().instances++;
                }
                break;
            case (int)ITEMLIST.MOREXP:
                gameObject.AddComponent<ItemMOREXP>();
                break;
            case (int)ITEMLIST.FAMILIAR:
                gameObject.AddComponent<ItemFAMILIAR>();
                break;
            case (int)ITEMLIST.HOMINGFAMILIAR:
                gameObject.AddComponent<ItemHOMINGFAMILIAR>();
                break;
            case (int)ITEMLIST.AUTOFAMILIAR:
                gameObject.AddComponent<ItemAUTOFAMILIAR>();
                break;
            case (int)ITEMLIST.RANDOMSHOT:
                gameObject.AddComponent<ItemRANDOMSHOT>();
                break;
            case (int)ITEMLIST.SAWSHOT:
                if (gameObject.GetComponent<ItemSAWSHOT>() == null)
                {
                    gameObject.AddComponent<ItemSAWSHOT>();
                }
                else
                {
                    gameObject.GetComponent<ItemSAWSHOT>().instances++;
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            Debug.Log("POOP! HAHA!");
            itemGained = col.gameObject.GetComponent<itemPedestal>().itemChosen;
            for (int i = 0; i < noToGive; i++)
            {
                itemsHeld.Add(itemGained);
                ApplyItems();
            }
        }
    }
}
