using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPedestal : MonoBehaviour
{
    public int itemChosen;
    int minRange = 21;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    int maxRange;
    GameObject[] gos;
    GameObject master;
    bool cursed = false;
    int curseType = -2;

    public string description;
    public bool enemiesCanUse;

    int[] specialItemWeights = new int[] { 20, 20, 20, 10, 10, 10, 4, 1 }; //{ 20, 20, 20, 10, 10, 10, 4, 1 };
    public int specialItemWeightsSum;

    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("bigFuckingMasterObject");
        maxRange = master.GetComponent<EntityReferencerGuy>().numItemsExist;
        maxRange = 22;
        itemChosen = Random.Range(minRange, maxRange);
        spriteRenderer.sprite = spriteArray[itemChosen];
        gos = GameObject.FindGameObjectsWithTag("item");
        GameObject.Find("newPlayer").GetComponent<getItemDescription>().itemsExist = true;
        Invoke(nameof(SetDescription), 0.1f);

        foreach (int i in specialItemWeights)
        {
            specialItemWeightsSum += i;
        }
    }

    void Update()
    {
        foreach (GameObject go in gos)
        {
            if (go.GetComponent<itemPedestal>().itemChosen == itemChosen && go != gameObject)
            {
                Debug.Log("WOw, something was fucked up!!!!!!!!!!!!");
                itemChosen = Mathf.RoundToInt(Random.Range(minRange, maxRange));
                spriteRenderer.sprite = spriteArray[itemChosen];
            }
        }
    }

    void SetDescription()
    {
        gameObject.GetComponent<ItemDescriptions>().itemChosen = itemChosen;
        gameObject.GetComponent<ItemDescriptions>().getItemDescription();
        description = gameObject.GetComponent<ItemDescriptions>().itemDescription;

        // this is for cursed items!
        curseType = -2;
        enemiesCanUse = gameObject.GetComponent<ItemDescriptions>().enemiesCanUse;
        int sproinkle = 2;//Random.Range(1, 21);
        if (sproinkle == 1)
        {
            int randomWacky = Random.Range(0, specialItemWeightsSum);
            int currentWeightSum = 0;
            for (int i = 0; i < specialItemWeights.Length; i++)
            {
                currentWeightSum += specialItemWeights[i];
                if (randomWacky < currentWeightSum)
                {
                    curseType = i;
                    Debug.Log("curse type: " + curseType.ToString());
                    break;
                }
            }
        }

        switch (curseType)
        {
            case 0:
                spriteRenderer.color = Color.magenta - new Color(0.3f, 0.2f, 0.2f, 0);
                break;
            case 1:
                spriteRenderer.color = Color.blue - new Color(0.3f, 0.2f, 0.2f, 0);
                break;
            case 2:
                spriteRenderer.color = Color.blue;
                break;
            case 3:
                spriteRenderer.color = Color.red;
                break;
            case 4:
                spriteRenderer.color = Color.magenta;
                break;
            case 5:
                spriteRenderer.color = Color.cyan;
                break;
            case 6: // gives 3 of the item.
                spriteRenderer.color = Color.green;
                break;
            case 7: // gives 10 of the item.
                spriteRenderer.color = Color.yellow;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GiveDaItem(col.gameObject);
        }
    }

    public void GiveDaItem(GameObject barry)
    {
        int dunkey = -5;
        switch (curseType)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                {
                    barry.GetComponent<ItemHolder>().itemGained = itemChosen;
                    barry.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
                    barry.GetComponent<ItemHolder>().ApplyItems();
                }
                master.GetComponent<ItemHolder>().itemGained = itemChosen;
                master.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
                break;
            case 1:
                //dunkey = itemChosen;
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                for (int i = 0; i < 3; i++)
                {
                    barry.GetComponent<ItemHolder>().itemGained = itemChosen;
                    barry.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
                    barry.GetComponent<ItemHolder>().ApplyItems();
                }
                break;
            case 5:
                break;
            case 6: // gives 3 of the item.
                for (int i = 0; i < 3; i++)
                {
                    barry.GetComponent<ItemHolder>().itemGained = itemChosen;
                    barry.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
                    barry.GetComponent<ItemHolder>().ApplyItems();
                }
                break;
            case 7: // gives 10 of the item.
                for (int i = 0; i < 10; i++)
                {
                    barry.GetComponent<ItemHolder>().itemGained = itemChosen;
                    barry.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
                    barry.GetComponent<ItemHolder>().ApplyItems();
                }
                break;
        }

        barry.GetComponent<OtherStuff>().ApplyItemCurse(curseType, dunkey);

        foreach (GameObject go in gos)
        {
            Destroy(go);
            GameObject.Find("newPlayer").GetComponent<getItemDescription>().itemsExist = false;
        }
    }
}
