using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class giveEnemySpecificItem : MonoBehaviour
{
    public List<string> itemNameToAdd = new List<string>();

    // For some reason if you just fucking add an item the enemy's list, i.e. just do itemsHeld.Add(), it also adds it to the master, which fucks it all up.
    // So instead we do this. Nice one John Unity.
    void Start()
    {
        List<int> fuckedItems = new List<int>();
        foreach (int item in gameObject.GetComponent<ItemHolder>().itemsHeld)
        {
            fuckedItems.Add(item);
        }

        foreach (string name in itemNameToAdd)
        {
            int itemToAdd = (int)Enum.Parse(typeof(ITEMLIST), name, false);
            fuckedItems.Add(itemToAdd);
            gameObject.GetComponent<ItemHolder>().itemGained = itemToAdd;
            gameObject.GetComponent<ItemHolder>().ApplyItems();
        }

        gameObject.GetComponent<ItemHolder>().itemsHeld = fuckedItems;
    }
}
