using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemHolder : MonoBehaviour
{
    public List<int> itemsHeld = new List<int>();
    public GameObject master;
    public int itemGained;
    public int noToGive = 1;
    int timermf;

    void Start()
    {
        master = EntityReferencerGuy.Instance.master;
        ApplyAll();
    }

    public void ApplyAll()
    {
        SendMessage("itemsAdded", true);
        foreach (int item in itemsHeld)
        {
            itemGained = item;
            ApplyItems();
        }

        //if (gameObject.tag == "Player")
        //{
        //    MakeEpicBullets();
        //}
    }

    public void ApplyItems()
    {
        if (gameObject.GetComponent<weaponType>() != null && gameObject.GetComponent<weaponType>().spawnedBat != null)
        {
            GameObject battest = gameObject.GetComponent<weaponType>().spawnedBat;
            battest.GetComponent<ItemHolder>().itemsHeld = itemsHeld;
            battest.GetComponent<ItemHolder>().itemGained = itemGained;
            battest.GetComponent<ItemHolder>().ApplyItems();
        }

        // Converts the itemgained to the name/type of the script to add.
        string itemAddedName = Enum.GetName(typeof(ITEMLIST), itemGained);
        string scriptName = "Item" + itemAddedName;
        Type scriptType = Type.GetType(scriptName);

        // Gets whether to add a new instance of the script or not when stacking.
        master.GetComponent<ItemDescriptions>().itemChosen = itemGained;
        master.GetComponent<ItemDescriptions>().getItemDescription();
        bool addNewScriptForNewInstance = master.GetComponent<ItemDescriptions>().addNewScriptForNewInstance;
        int whoToGiveTo = master.GetComponent<ItemDescriptions>().whatUses;
        Debug.Log("Who to give to: " + whoToGiveTo.ToString());

        // Checks what exactly to give the item to, and hence only applies the item when it is required.
        bool doGiveItem = false;
        if (((gameObject.GetComponent<Attack>()) && (whoToGiveTo == (int)ITEMOWNERS.ALL || whoToGiveTo == (int)ITEMOWNERS.BEING)) || // For 'being'
           ((gameObject.GetComponent<Attack>() != null && (whoToGiveTo == (int)ITEMOWNERS.ALL || whoToGiveTo == (int)ITEMOWNERS.CANSHOOT))) || // For 'canshoot' - required so orbitals n shit don't get gunners.
           ((gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet") && gameObject.GetComponent<Attack>() == null && (whoToGiveTo == (int)ITEMOWNERS.ALL || whoToGiveTo == (int)ITEMOWNERS.BULLET)) || // For 'bullet'
           ((gameObject.tag == "Player" || gameObject.tag == "MasterObject") && (whoToGiveTo == (int)ITEMOWNERS.MASTERANDPLAYER))) // For 'masterandplayer'
        {
            doGiveItem = true;
        }

        Debug.Log("well okay die" + gameObject.name);


        // Applies the item, either adding a new instance of the script or incrementing a script's 'instances' value.
        if (doGiveItem)
        {
            if (addNewScriptForNewInstance)
            {
                gameObject.AddComponent(scriptType);
            }
            else
            {
                if (gameObject.GetComponent(scriptType) == null)
                {
                    gameObject.AddComponent(scriptType);
                }
                else
                {
                    SendMessage("IncreaseInstances", scriptName);
                }
            }
        }

        //    case (int)ITEMLIST.DODGEROLL:
        //        if (gameObject.tag == "Player")
        //        {
        //            gameObject.GetComponent<NewPlayerMovement>().mouseAltMode = 0;
        //            master.GetComponent<ThirdEnemySpawner>().playerBannedDodge = (int)ITEMLIST.DODGEROLL;
        //        }
        //        break;
        //    case (int)ITEMLIST.SHOULDERBASH:
        //        if (gameObject.tag == "Player")
        //        {
        //            gameObject.GetComponent<NewPlayerMovement>().mouseAltMode = 1;
        //            master.GetComponent<ThirdEnemySpawner>().playerBannedDodge = (int)ITEMLIST.SHOULDERBASH;
        //        }
        //        break;


        //    case (int)ITEMLIST.PISTOL:
        //        if (gameObject.GetComponent<weaponType>() != null)
        //        {
        //            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.PISTOL;
        //            gameObject.GetComponent<weaponType>().SetWeapon();

        //            if (gameObject.tag == "Player")
        //            {
        //                master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.PISTOL;
        //            }
        //        }
        //        break;
        //    case (int)ITEMLIST.GRENADELAUNCHER:
        //        if (gameObject.GetComponent<weaponType>() != null)
        //        {
        //            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.GRENADELAUNCHER;
        //            gameObject.GetComponent<weaponType>().SetWeapon();

        //            if (gameObject.tag == "Player")
        //            {
        //                master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.GRENADELAUNCHER;
        //            }
        //        }
        //        break;
        //    case (int)ITEMLIST.LAZER:
        //        if (gameObject.GetComponent<weaponType>() != null)
        //        {
        //            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.LAZER;
        //            gameObject.GetComponent<weaponType>().SetWeapon();

        //            if (gameObject.tag == "Player")
        //            {
        //                master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.LAZER ;
        //            }
        //        }
        //        break;
        //    case (int)ITEMLIST.BAT:
        //        if (gameObject.GetComponent<weaponType>() != null)
        //        {
        //            gameObject.GetComponent<weaponType>().weaponHeld = (int)ITEMLIST.BAT;
        //            gameObject.GetComponent<weaponType>().SetWeapon();

        //            if (gameObject.tag == "Player")
        //            {
        //                master.GetComponent<ThirdEnemySpawner>().playerBannedWeapon = (int)ITEMLIST.BAT;
        //            }
        //        }
        //        break;




        //    case (int)ITEMLIST.CREEPSHOT:
        //        if (isBullet)
        //        {
        //            gameObject.AddComponent<ItemCREEPSHOT>();
        //        }
        //        break;
        //}
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.tag == "item")
    //    {
    //        GiveFunny(col.gameObject);
    //    }
    //}

    public void itemsAdded()
    {
        //stop saying there is no fucking reciever
    }

    public void GiveFunny(GameObject bumbino)
    {
        bool itemIsPassive = false;
        itemGained = bumbino.GetComponent<itemPedestal>().itemChosen;
        if (bumbino.GetComponent<itemPedestal>().chosenQuality == (int)ITEMTIERS.WEAPON || bumbino.GetComponent<itemPedestal>().chosenQuality == (int)ITEMTIERS.DODGE) // Makes it so extra copies of items only get applied if they're NOT a weapon or dodge, otherwise you could waste them.
        {
            itemsHeld.Add(itemGained);
            ApplyItems();
        }
        else
        {
            itemIsPassive = true;
            for (int i = 0; i < noToGive; i++)
            {
                itemsHeld.Add(itemGained);
                ApplyItems();
            }
        }

        Debug.Log(itemIsPassive.ToString());

        SendMessage("itemsAdded", itemIsPassive);
    }

    // The following is all for the old (bad) bullet pool system that wasn't very efficient anyway.
    
    //public void MakeEpicBullets()
    //{
    //    if (gameObject.tag == "Player")
    //    {
    //        Destroy(playerBulletPrefab);
    //        Destroy(enemyBulletPrefab);

    //        playerBulletPrefab = Instantiate(playerBullet, new Vector3(999999, 999999, 999999), transform.rotation);
    //        playerBulletPrefab.GetComponent<ItemHolder>().itemsHeld = itemsHeld;
    //        Rigidbody2D playerBRB = playerBulletPrefab.GetComponent<Rigidbody2D>();
    //        playerBRB.simulated = false;
    //        playerBRB.GetComponent<DealDamage>().master = gameObject.GetComponent<DealDamage>().master;
    //        playerBRB.GetComponent<DealDamage>().owner = gameObject;
    //        if (gameObject.GetComponent<weaponType>().weaponHeld == (int)ITEMLIST.GRENADELAUNCHER)
    //        {
    //            playerBulletPrefab.AddComponent<explodeOnHit>();
    //        }
    //        gameObject.GetComponent<Attack>().Bullet = playerBulletPrefab;
    //        enemyBulletPrefab = Instantiate(enemyBullet, new Vector3(9999999, 9999999, 9999999), transform.rotation);
    //        enemyBulletPrefab.GetComponent<ItemHolder>().itemsHeld = master.GetComponent<ItemHolder>().itemsHeld;
    //        Rigidbody2D enemyBRB = enemyBulletPrefab.GetComponent<Rigidbody2D>();
    //        master.GetComponent<ThirdEnemySpawner>().enemyBullet = enemyBulletPrefab;
    //        enemyBRB.simulated = false;

    //        Invoke(nameof(setBulletsToClones), 0.1f);
    //    }
    //}

    //void setBulletsToClones()
    //{
    //    playerBulletPrefab.GetComponent<DealDamage>().isBulletClone = true;
    //    playerBulletPrefab.GetComponent<DealDamage>().isSourceBullet = true;
    //    enemyBulletPrefab.GetComponent<DealDamage>().isBulletClone = true;
    //    enemyBulletPrefab.GetComponent<DealDamage>().isSourceBullet = true;
    //}

    //void Update()
    //{
    //    if (gameObject.tag == "Player")
    //    {
    //        playerBulletPrefab.transform.position = new Vector3(999999, 999999, 999999);
    //        enemyBulletPrefab.transform.position = new Vector3(9999999, 9999999, 9999999);
    //    }
    //}
}
