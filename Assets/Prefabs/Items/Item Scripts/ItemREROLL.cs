using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemREROLL : ItemScript
{
    int[] itemRarities;

    void Start()
    {
        itemRarities = new int[gameObject.GetComponent<ItemHolder2>().itemsHeld.Count];
        int i = 0;

        // Iterates through every held item, storing its rarity and reducing instances.
        foreach (ItemSOInst item in gameObject.GetComponent<ItemHolder2>().itemsHeld)
        {
            itemRarities[i] = item.rarity;
            MonoScript itemScript = item.itemScript;
            System.Type m_ScriptClass = itemScript.GetClass();
            if (gameObject.GetComponent(m_ScriptClass) != null)
            {
                Component newComponent = gameObject.GetComponent(m_ScriptClass);
                ItemScript scriptToFunny = (ItemScript)newComponent;
                scriptToFunny.RemoveInstance();
            }
            i++;
        }

        // Actually removes the items from the lists.
        gameObject.GetComponent<ItemHolder2>().itemsHeld.Clear();
        gameObject.GetComponent<ItemHolder2>().itemScripts.Clear();
        gameObject.GetComponent<ItemHolder2>().itemsHeldTransferred = new ItemSOInst[itemRarities.Length];

        int j = 0;

        // Gets a new set of items with equal rarities to the previous items.
        foreach (int rarity in itemRarities)
        {
            int newItemRarity = -5;
            ItemSOInst newItem = null;

            while (newItemRarity != rarity)
            {
                newItem = EntityReferencerGuy.Instance.allItems[Random.Range(0, EntityReferencerGuy.Instance.allItems.Length)];
                newItemRarity = newItem.rarity;
            }

            gameObject.GetComponent<ItemHolder2>().itemsHeldTransferred[j] = newItem;
            j++;
        }

        gameObject.GetComponent<ItemHolder2>().ApplyAll();
        Destroy(this);
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    //gameObject.GetComponent<OtherStuff>().Sprinkle(0);
    //    if (gameObject.tag == "Player" || gameObject.tag == "MasterObject")
    //    {
    //        master = EntityReferencerGuy.Instance.master;
    //        //maxRange = master.GetComponent<EntityReferencerGuy>().numItemsExist;
    //        Debug.Log("Boingser");
    //        foreach (int item in gameObject.GetComponent<ItemHolder>().itemsHeld)
    //        {
    //            EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().itemChosen = item;
    //            EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().getItemDescription();
    //            int oldItemQuality = EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().quality;
    //            int itemChosen = (int)ITEMLIST.REROLL;
    //            int newItemQuality = 0;
    //            while (itemChosen == (int)ITEMLIST.REROLL || oldItemQuality != newItemQuality)
    //            {
    //                itemChosen = Random.Range(0, (int)ITEMLIST.CREEPSHOT);
    //                EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().itemChosen = itemChosen;
    //                EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().getItemDescription();
    //                newItemQuality = EntityReferencerGuy.Instance.master.GetComponent<ItemDescriptions>().quality;
    //            }
    //            Debug.Log("itemChosen: " + itemChosen.ToString() + " / itemQuality: " + newItemQuality.ToString() + " / oldItemQuality: " + oldItemQuality.ToString());
    //            newItems.Add(itemChosen);
    //        }

    //        for (int i = 1; i < instances; i++) // For adding extra items if the player picks up a 3x of this!
    //        {
    //            int itemChosen = Random.Range(0, (int)ITEMLIST.CREEPSHOT);
    //            while (itemChosen == (int)ITEMLIST.REROLL)
    //            {
    //                itemChosen = Random.Range(0, (int)ITEMLIST.CREEPSHOT);
    //            }
    //            newItems.Add(itemChosen);
    //        }

    //        gameObject.GetComponent<ItemHolder>().itemsHeld = newItems;
    //        Invoke(nameof(POOPOO), 0.1f);
    //        Invoke(nameof(KILL), 0.2f);
    //    }
    //}

    //public void POOPOO()
    //{
    //    gameObject.GetComponent<ItemHolder>().ApplyAll();
    //}

    //public void KILL()
    //{
    //    Destroy(this);
    //}
}
