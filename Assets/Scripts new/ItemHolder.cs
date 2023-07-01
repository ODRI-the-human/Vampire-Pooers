using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemHolder : MonoBehaviour
{
    public List<int> itemsHeld = new List<int>();
    public GameObject master;
    public int itemGained;
    public int weaponHeld;
    public int noToGive = 1;
    int timermf;

    public GameObject spawnedBat;

    //Stores the PREVIOUS stats of the weapon held, needed for when enemies use Dark Arts.
    public int previousFireType;
    public float previousFireTimerLengthMLT;

    void Start()
    {
        master = EntityReferencerGuy.Instance.master;

        if (gameObject.GetComponent<Attack>() != null)
        {
            previousFireType = gameObject.GetComponent<Attack>().specialFireType;
            previousFireTimerLengthMLT = gameObject.GetComponent<Attack>().fireTimerLengthMLT;
        }

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
        SetWeapon(weaponHeld);

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
        //Debug.Log("Who to give to: " + whoToGiveTo.ToString());

        // Checks what exactly to give the item to, and hence only applies the item when it is required.
        bool doGiveItem = false;
        if (((gameObject.tag == "Player" || gameObject.tag == "Hostile") && (whoToGiveTo == (int)ITEMOWNERS.ALL || whoToGiveTo == (int)ITEMOWNERS.BEING)) || // For 'being'
           ((gameObject.GetComponent<Attack>() != null && (whoToGiveTo == (int)ITEMOWNERS.ALL || whoToGiveTo == (int)ITEMOWNERS.CANSHOOT))) || // For 'canshoot' - required so orbitals n shit don't get gunners.
           ((gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet") && gameObject.GetComponent<Attack>() == null && (whoToGiveTo == (int)ITEMOWNERS.ALL || whoToGiveTo == (int)ITEMOWNERS.BULLET)) || // For 'bullet'
           ((gameObject.tag == "Player" || gameObject.tag == "MasterObject") && (whoToGiveTo == (int)ITEMOWNERS.MASTERANDPLAYER))) // For 'masterandplayer'
        {
            doGiveItem = true;
        }

        //Debug.Log("well okay die" + gameObject.name);


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
    }

    public void SetWeapon(int weaponToGive)
    {
        if (gameObject.GetComponent<BatVisual>() != null)
        {
            gameObject.GetComponent<BatVisual>().Kill();
        }

        Debug.Log("weapon set xd");
        weaponHeld = weaponToGive;

        switch (weaponToGive)
        {
            case 0: // For resetting enemies' shit back to their normie settings.
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = previousFireType;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = previousFireTimerLengthMLT;
                }
                break;
            case (int)ITEMLIST.PISTOL:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
                    gameObject.GetComponent<Attack>().shotSpeed = 15;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.regularShotParticle;
                }
                break;
            case (int)ITEMLIST.GRENADELAUNCHER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 0;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.5f;
                    gameObject.GetComponent<Attack>().shotSpeed = 45;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.regularShotParticle;
                }
                if (gameObject.GetComponent<Attack>() == null)
                {
                    gameObject.GetComponent<DealDamage>().finalDamageMult *= 1.5f;
                    gameObject.AddComponent<explodeOnHit>();
                }
                break;
            case (int)ITEMLIST.LAZER:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().specialFireType = 5;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1.2f;
                    gameObject.GetComponent<Attack>().shotSpeed = 10;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.empty;
                }
                break;
            case (int)ITEMLIST.BAT:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    //gameObject.GetComponent<NewPlayerMovement>().AttackStatus(false); // otherwise the player is slow (silly)
                    //gameObject.GetComponent<Attack>().specialFireType = 6;
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
                    gameObject.GetComponent<Attack>().holdDownToShoot = false;
                    //spawnedBat = Instantiate(EntityReferencerGuy.Instance.bat);
                    //spawnedBat.GetComponent<faceInFunnyDirection>().owner = gameObject;
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.empty;
                    gameObject.AddComponent<BatVisual>();

                    gameObject.GetComponent<Attack>().meleeHitObj = EntityReferencerGuy.Instance.batHitbox;
                    gameObject.GetComponent<Attack>().hitboxSpawnDelay = 0.1f;
                    gameObject.GetComponent<Attack>().specialFireType = 3;
                }
                break;
            case (int)ITEMLIST.DARKARTS:
                if (gameObject.GetComponent<Attack>() != null)
                {
                    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 0.5f;
                    gameObject.GetComponent<Attack>().holdDownToShoot = true;
                    gameObject.GetComponent<Attack>().shotParticles = EntityReferencerGuy.Instance.empty;
                    gameObject.AddComponent<BatVisual>();

                    gameObject.GetComponent<Attack>().meleeHitObj = EntityReferencerGuy.Instance.darkArtHitbox;
                    gameObject.GetComponent<Attack>().hitboxSpawnDelay = 0.1f;
                    gameObject.GetComponent<Attack>().specialFireType = 3;
                }
                break;
        }

        SendMessage("itemsAdded", false);
    }

    public void itemsAdded()
    {
        //stop saying there is no fucking reciever
    }

    public void GiveFunny(GameObject bumbino)
    {
        bool itemIsPassive = false;
        itemGained = bumbino.GetComponent<itemPedestal>().itemChosen;
        if (bumbino.GetComponent<itemPedestal>().chosenQuality == (int)ITEMTIERS.WEAPON) // Makes it so extra copies of items only get applied if they're NOT a weapon or dodge, otherwise you could waste them.
        {
            SetWeapon(itemGained);
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
        //Debug.Log(itemIsPassive.ToString());

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
