using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public List<int> itemsHeld = new List<int>();
    public GameObject master;
    public int itemGained;
    public int noToGive = 1;
    int timermf;

    bool isBullet = false;
    bool isGuy = true;

    public bool doTheShit = true;

    public GameObject playerBullet;
    public GameObject enemyBullet;

    GameObject playerBulletPrefab;
    GameObject enemyBulletPrefab;

    void Start()
    {
        master = GameObject.Find("bigFuckingMasterObject");
        playerBullet = master.GetComponent<EntityReferencerGuy>().playerBullet;
        enemyBullet = master.GetComponent<EntityReferencerGuy>().enemyBullet;
        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
        {
            isGuy = false;
            isBullet = true;
        }

        if (doTheShit)
        {
            ApplyAll();
        }
        if (gameObject.tag == "Player")
        {
            MakeEpicBullets();
        }

        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
        {
            isGuy = false;
            isBullet = true;
        }
    }

    public void ApplyAll()
    {
        foreach (int item in itemsHeld)
        {
            itemGained = item;
            ApplyItems();
        }
    }

    public void ApplyItems()
    {
        switch (itemGained)
        {
            case (int)ITEMLIST.HP25:
                if (isGuy)
                {
                    gameObject.AddComponent<ItemHP25>();
                }
                break;
            case (int)ITEMLIST.HP50:
                if (isGuy)
                {
                    gameObject.AddComponent<ItemHP50>();
                }
                break;
            case (int)ITEMLIST.DMGADDPT5:
                gameObject.AddComponent<ItemDMGADDPT5>();
                break;
            case (int)ITEMLIST.DMGMLT2:
                gameObject.AddComponent<ItemDMGMLT2>();
                break;
            case (int)ITEMLIST.FIRERATE:
                if (isGuy)
                {
                    gameObject.AddComponent<ItemFIRERATE>();
                }
                break;
            case (int)ITEMLIST.SOY:
                gameObject.AddComponent<ItemSOY>();
                break;
            case (int)ITEMLIST.HOMING:
                if (isBullet)
                {
                    if (gameObject.GetComponent<ItemHOMING>() == null)
                    {
                        gameObject.AddComponent<ItemHOMING>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemHOMING>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.ATG:
                if (isBullet)
                {
                    if (gameObject.GetComponent<ItemATG>() == null)
                    {
                        gameObject.AddComponent<ItemATG>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemATG>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.MORESHOT:
                if (isGuy)
                {
                    gameObject.AddComponent<ItemMORESHOT>();
                }
                break;
            case (int)ITEMLIST.WAPANT:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemWAPANT>() == null)
                    {
                        gameObject.AddComponent<ItemWAPANT>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemWAPANT>().wapantTimerLength /= 1.2f;
                        gameObject.GetComponent<ItemWAPANT>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.HOLYMANTIS:
                if (isGuy)
                {
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
                }
                break;
            case (int)ITEMLIST.CONVERTER:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemCONVERTER>() == null)
                    {
                        gameObject.AddComponent<ItemCONVERTER>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemCONVERTER>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.EASIERTIMES:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemEASIERTIMES>() == null)
                    {
                        gameObject.AddComponent<ItemEASIERTIMES>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemEASIERTIMES>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.STOPWATCH:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemSTOPWATCH>() == null)
                    {
                        gameObject.AddComponent<ItemSTOPWATCH>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemSTOPWATCH>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.BOUNCY:
                if (isBullet)
                {
                    if (gameObject.GetComponent<ItemBOUNCY>() == null)
                    {
                        gameObject.AddComponent<ItemBOUNCY>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemBOUNCY>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.FOURDIRMARTY:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemFOURDIRMARTY>() == null)
                    {
                        gameObject.AddComponent<ItemFOURDIRMARTY>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemFOURDIRMARTY>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.PIERCING:
                if (isBullet)
                {
                    if (gameObject.GetComponent<ItemPIERCING>() == null)
                    {
                        gameObject.AddComponent<ItemPIERCING>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemPIERCING>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.CREEP:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemCREEP>() == null)
                    {
                        gameObject.AddComponent<ItemCREEP>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemCREEP>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.DODGESPLOSION:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemDODGESPLOSION>() == null)
                    {
                        gameObject.AddComponent<ItemDODGESPLOSION>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemDODGESPLOSION>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.BETTERDODGE:
                if (isGuy)
                {
                    gameObject.AddComponent<ItemBETTERDODGE>();
                }
                break;
            case (int)ITEMLIST.ORBITAL1:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemORBITAL1>() == null)
                    {
                        gameObject.AddComponent<ItemORBITAL1>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemORBITAL1>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.ORBITAL2:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemORBITAL2>() == null)
                    {
                        gameObject.AddComponent<ItemORBITAL2>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemORBITAL2>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.SPLIT:
                if (isBullet)
                {
                    if (gameObject.GetComponent<ItemSPLIT>() == null)
                    {
                        gameObject.AddComponent<ItemSPLIT>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemSPLIT>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.CONTACT:
                if (isBullet)
                {
                    if (gameObject.GetComponent<ItemCONTACT>() == null)
                    {
                        gameObject.AddComponent<ItemCONTACT>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemCONTACT>().instances++;
                    }
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
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemPOISONSPLOSM>() == null)
                    {
                        gameObject.AddComponent<ItemPOISONSPLOSM>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemPOISONSPLOSM>().instances++;
                    }
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
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemBERSERK>() == null)
                    {
                        gameObject.AddComponent<ItemBERSERK>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemBERSERK>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.HEALMLT:
                if (isGuy)
                {
                    gameObject.AddComponent<ItemHEALMLT>();
                }
                break;
            case (int)ITEMLIST.PERFECTHEAL:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemPERFECTHEAL>() == null)
                    {
                        gameObject.AddComponent<ItemPERFECTHEAL>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemPERFECTHEAL>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.REROLL:
                if (isGuy)
                {
                    gameObject.AddComponent<ItemREROLL>();
                }
                break;
            case (int)ITEMLIST.BRICK:
                if (isBullet)
                {
                    if (gameObject.GetComponent<ItemBRICK>() == null)
                    {
                        gameObject.AddComponent<ItemBRICK>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemBRICK>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.BETTERLEVEL:
                if (isGuy)
                {
                    gameObject.AddComponent<ItemBETTERLEVEL>();
                }
                break;
            case (int)ITEMLIST.EXTRAITEMLEVEL:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemEXTRAITEMLEVEL>() == null)
                    {
                        gameObject.AddComponent<ItemEXTRAITEMLEVEL>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemEXTRAITEMLEVEL>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.MORELEVELSTATS:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemMORELEVELSTATS>() == null)
                    {
                        gameObject.AddComponent<ItemMORELEVELSTATS>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemMORELEVELSTATS>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.HEALTHXP:
                if (isGuy)
                {
                    if (gameObject.GetComponent<ItemHEALTHXP>() == null)
                    {
                        gameObject.AddComponent<ItemHEALTHXP>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemHEALTHXP>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.LEVELHEAL:
                if (isGuy)
                {

                    gameObject.AddComponent<ItemLEVELHEAL>();
                }
                break;
            case (int)ITEMLIST.DAGGERTHROW:
                if (isGuy)
                {

                    if (gameObject.GetComponent<ItemDAGGERTHROW>() == null)
                    {
                        gameObject.AddComponent<ItemDAGGERTHROW>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemDAGGERTHROW>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.MOREXP:
                if (isGuy)
                {

                    gameObject.AddComponent<ItemMOREXP>();
                }
                break;
            case (int)ITEMLIST.FAMILIAR:
                if (isGuy)
                {

                    gameObject.AddComponent<ItemFAMILIAR>();
                }
                break;
            case (int)ITEMLIST.HOMINGFAMILIAR:
                if (isGuy)
                {

                    gameObject.AddComponent<ItemHOMINGFAMILIAR>();
                }
                break;
            case (int)ITEMLIST.AUTOFAMILIAR:
                if (isGuy)
                {

                    gameObject.AddComponent<ItemAUTOFAMILIAR>();
                }
                break;
            case (int)ITEMLIST.SAWSHOT:
                if (isBullet)
                {

                    if (gameObject.GetComponent<ItemSAWSHOT>() == null)
                    {
                        gameObject.AddComponent<ItemSAWSHOT>();
                    }
                    else
                    {
                        gameObject.GetComponent<ItemSAWSHOT>().instances++;
                    }
                }
                break;
            case (int)ITEMLIST.LUCKIER:
                gameObject.AddComponent<ItemLUCKIER>();
                break;
            case (int)ITEMLIST.MORECRITS:
                gameObject.AddComponent<ItemMORECRITS>();
                break;
            case (int)ITEMLIST.BETTERCRITS:
                gameObject.AddComponent<ItemBETTERCRITS>();
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
            MakeEpicBullets();
        }
    }

    void MakeEpicBullets()
    {
        Destroy(playerBulletPrefab);
        Destroy(enemyBulletPrefab);

        playerBulletPrefab = Instantiate(playerBullet, new Vector3(999999, 999999, 999999), transform.rotation);
        playerBulletPrefab.GetComponent<ItemHolder>().itemsHeld = itemsHeld;
        Rigidbody2D playerBRB = playerBulletPrefab.GetComponent<Rigidbody2D>();
        playerBRB.simulated = false;
        playerBRB.GetComponent<DealDamage>().master = gameObject.GetComponent<DealDamage>().master;
        playerBRB.GetComponent<DealDamage>().owner = gameObject;
        gameObject.GetComponent<Attack>().Bullet = playerBulletPrefab;
        enemyBulletPrefab = Instantiate(enemyBullet, new Vector3(9999999, 9999999, 9999999), transform.rotation);
        enemyBulletPrefab.GetComponent<ItemHolder>().itemsHeld = master.GetComponent<ItemHolder>().itemsHeld;
        Rigidbody2D enemyBRB = enemyBulletPrefab.GetComponent<Rigidbody2D>();
        master.GetComponent<ThirdEnemySpawner>().enemyBullet = enemyBulletPrefab;
        enemyBRB.simulated = false;
    }

    void Update()
    {
        if (gameObject.tag == "Player")
        {
            playerBulletPrefab.transform.position = new Vector3(999999, 999999, 999999);
            enemyBulletPrefab.transform.position = new Vector3(9999999, 9999999, 9999999);
        }
    }
}
