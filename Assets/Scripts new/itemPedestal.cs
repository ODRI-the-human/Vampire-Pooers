using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPedestal : MonoBehaviour
{
    public int itemChosen;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    int minRange = 0;
    int maxRange;
    GameObject[] gos;
    GameObject master;
    bool cursed = false;

    public string description;
    public bool enemiesCanUse;

    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("bigFuckingMasterObject");
        maxRange = master.GetComponent<EntityReferencerGuy>().numItemsExist;
        itemChosen = Random.Range(minRange, maxRange);
        spriteRenderer.sprite = spriteArray[itemChosen];
        gos = GameObject.FindGameObjectsWithTag("item");
        GameObject.Find("newPlayer").GetComponent<getItemDescription>().itemsExist = true;
        Invoke(nameof(SetDescription), 0.1f);
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
        enemiesCanUse = gameObject.GetComponent<ItemDescriptions>().enemiesCanUse;
        int sproinkle = Random.Range(1, 21);
        if (sproinkle == 1 && enemiesCanUse)
        {
            cursed = true;
            transform.localScale *= 2;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (cursed)
            {
                col.gameObject.GetComponent<ItemHolder>().itemGained = itemChosen;
                col.gameObject.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
                col.gameObject.GetComponent<ItemHolder>().ApplyItems();
                master.GetComponent<ItemHolder>().itemGained = itemChosen;
                master.GetComponent<ItemHolder>().itemsHeld.Add(itemChosen);
            }

            foreach (GameObject go in gos)
            {
                Destroy(go);
                GameObject.Find("newPlayer").GetComponent<getItemDescription>().itemsExist = false;
            }
        }
    }
}
