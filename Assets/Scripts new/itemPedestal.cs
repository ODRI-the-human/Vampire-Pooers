using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPedestal : MonoBehaviour
{
    public bool doRandomiseItem = true; // Make this false if the object that spawns this specifically wants it to spawn a particular item.

    public int itemChosen;
    int minRange = 0;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    int maxRange;
    GameObject[] gos;
    public GameObject master;
    bool cursed = false;
    public int curseType = -2;

    public string description;
    public string curseDescription;
    public bool enemiesCanUse;

    int[] specialItemWeights = new int[] { 20, 20, 20, 10, 10, 10, 4, 1 }; //{ 20, 20, 20, 10, 10, 10, 4, 1 };
    int[] itemQualWeights = new int[] { 20, 10, 2, 0 };
    public int chosenQuality;
    public int qualityWeightsSum;
    public int randomedQuality; // This stores the quality of the item the random.range picked when rolling for an item.
    public int specialItemWeightsSum;

    // Start is called before the first frame update
    void Start()
    {
        //bannedItems.Add((int)ITEMLIST.HP25);
        master = EntityReferencerGuy.Instance.master;
        maxRange = (int)ITEMLIST.CREEPSHOT;
        gameObject.GetComponent<Collider2D>().enabled = false;
        //maxRange = 5;
        gos = GameObject.FindGameObjectsWithTag("item");

        GameObject[] gosser = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject guy in gosser)
        {
            guy.GetComponent<getItemDescription>().itemsExist = true;
        }

        //Invoke(nameof(SetDescription), 0.1f);

        foreach (int i in specialItemWeights)
        {
            specialItemWeightsSum += i;
        }
        
        if (doRandomiseItem)
        {
            GetARandomItem();
        }
        else
        {
            spriteRenderer.sprite = spriteArray[itemChosen];
        }

        SetDescription();

        Invoke(nameof(enableHitbox), 0.5f);
    }

    void GetQuality()
    {
        qualityWeightsSum = 0;
        foreach (int i in itemQualWeights)
        {
            qualityWeightsSum += i;
        }

        int randomWacky = Random.Range(0, qualityWeightsSum);
        //Debug.Log(randomWacky.ToString());
        int currentWeightSum = 0;
        for (int i = 0; i < itemQualWeights.Length; i++)
        {
            currentWeightSum += itemQualWeights[i];
            if (randomWacky <= currentWeightSum)
            {
                chosenQuality = i;
                break;
            }
        }
    }

    void enableHitbox()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        if (doRandomiseItem)
        {
            foreach (GameObject go in gos)
            {
                bool isFine = true;

                //foreach (int item in bannedItems)
                //{
                //    if (item == itemChosen)
                //        isFine = false;
                //}

                if ((go.GetComponent<itemPedestal>().itemChosen == itemChosen && go != gameObject) || !isFine)
                {
                    Debug.Log("WOw, something was fucked up!!!!!!!!!!!!");
                    GetARandomItem();
                }
            }
        }
    }

    void GetARandomItem()
    {
        randomedQuality = -5;

        while (randomedQuality != chosenQuality)
        {
            GetQuality();
            itemChosen = Mathf.RoundToInt(Random.Range(minRange, maxRange));
            spriteRenderer.sprite = spriteArray[itemChosen];

            master.GetComponent<ItemDescriptions>().itemChosen = itemChosen;
            master.GetComponent<ItemDescriptions>().getItemDescription();
            randomedQuality = master.GetComponent<ItemDescriptions>().quality;
        }
    }

    void SetDescription()
    {
        master.GetComponent<ItemDescriptions>().itemChosen = itemChosen;
        master.GetComponent<ItemDescriptions>().getItemDescription();
        description = master.GetComponent<ItemDescriptions>().itemDescription;

        curseType = -2;
        int sproinkle = Random.Range(0, 21); // Determines whether the item is cursed or not (1/20 chance)

        if (sproinkle == 5)
        {
            int randomWacky = Random.Range(0, specialItemWeightsSum);
            int currentWeightSum = 0;
            for (int i = 0; i < specialItemWeights.Length; i++)
            {
                currentWeightSum += specialItemWeights[i];
                if (randomWacky < currentWeightSum)
                {
                    curseType = i;
                    break;
                }
            }
        }

        master.GetComponent<ItemDescriptions>().GetCurseDescription(curseType);
        curseDescription = master.GetComponent<ItemDescriptions>().curseDescription;
        doRandomiseItem = false;
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        GiveDaItem(col.gameObject);
    //    }
    //}

    public void GiveDaItem(GameObject barry)
    {
        int numToGiveEnemies = 0;
        int numToGivePlayer = 0;

        switch (curseType)
        {
            case 0: // Gives player three of the item, gives enemies one.
                numToGiveEnemies = 1;
                numToGivePlayer = 3;
                break;
            case 1: // Get 1 of this item every time you pick up an item, lose 2 items on hit (perm)
                if (barry.GetComponent<lose2ItemPerm>() == null)
                {
                    barry.AddComponent<lose2ItemPerm>();
                    barry.GetComponent<lose2ItemPerm>().itemsToGiveOnRoundStart.Add(itemChosen);
                }
                else
                {
                    barry.GetComponent<lose2ItemPerm>().itemsToGiveOnRoundStart.Add(itemChosen);
                    barry.GetComponent<lose2ItemPerm>().numItemsToLose += 2;
                }
                // Need one thing to apply it the first time, another to make the player lose extra items/gain extra items.
                break;
            case 2: // Get 3 of the item, lose 5 random items (ONCE) if you get hit in the next 2 rounds.
                if (barry.GetComponent<lose5ItemTemp>() == null)
                {
                    barry.AddComponent<lose5ItemTemp>();
                }
                else
                {
                    barry.GetComponent<lose5ItemTemp>().roundsLeft.Add(2);
                    barry.GetComponent<lose5ItemTemp>().numItemsToLose += 5;
                }

                numToGivePlayer = 3;
                // Need one thing to apply it the first time, another to make the player lose extra items/gain extra items (and set the proper timers).
                break;
            case 3: // Give enemies one of the item, if an enemy dies in the next 2 rounds they can drop an item they hold.
                master.GetComponent<doMasterCurses>().numRoundsDropItemLeft = 2;
                numToGiveEnemies = 1;
                // Just need to enable the thingy on the master script, or reset the timer if it.
                break;
            case 4: // Get five of the item, but can't heal ever again.
                if (barry.GetComponent<disableHealing>() == null)
                {
                    barry.AddComponent<disableHealing>();
                }
                numToGivePlayer = 5;
                break;
            case 5: // Get three of the item, but die instantly if hit in the next 2 rounds.
                if (barry.GetComponent<killInstantly>() == null)
                {
                    barry.AddComponent<killInstantly>();
                }
                else
                {
                    barry.GetComponent<killInstantly>().roundsLeft = 2;
                }

                numToGivePlayer = 3;
                break;
            case 6: // gives 2 of the item.
                numToGivePlayer = 2;
                break;
            case 7: // gives 5 of the item.
                numToGivePlayer = 5;
                break;
        }

        for (int i = 1; i < numToGivePlayer; i++)
        {
            barry.GetComponent<ItemHolder>().itemGained = itemChosen;
            barry.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
            barry.GetComponent<ItemHolder>().ApplyItems();
        }

        for (int i = 0; i < numToGiveEnemies; i++)
        {
            //master.GetComponent<ItemHolder>().itemGained = itemChosen;
            master.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
        }

        foreach (GameObject go in gos)
        {
            Destroy(go);
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                player.GetComponent<getItemDescription>().itemsExist = false;
            }
        }
    }
}
