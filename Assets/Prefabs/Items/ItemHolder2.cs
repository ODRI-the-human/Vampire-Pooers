using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder2 : MonoBehaviour
{
    public List<ItemScript> itemScripts = new List<ItemScript>();

    public void OnHits(GameObject victim, GameObject responsible)
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnHit(victim, responsible);
        }
    }

    public void OnKills()
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnKill();
        }
    }

    public void OnHurts()
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnHurt();
        }
    }
    
    public void OnLevels()
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnLevel();
        }
    }

    public float DamageMult()
    {
        float damageMultiplier = 1f;

        foreach (ItemScript item in itemScripts)
        {
            damageMultiplier *= item.DamageMult();
        }

        return damageMultiplier;
    }
}
