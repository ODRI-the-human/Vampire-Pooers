using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemHolder2 : MonoBehaviour
{
    public List<ItemScript> itemScripts = new List<ItemScript>();
    public List<ItemSOInst> itemsHeld = new List<ItemSOInst>();
    public ItemSOInst[] itemsHeldTransferred; // Only attacks use this. otherwise stupid issues ok
    public int noToGive = 1;

    public void ApplyItem(ItemSOInst item)
    {
        for (int i = 0; i < noToGive; i++)
        {
            itemsHeld.Add(item);
            if ((item.addToProjectiles && gameObject.GetComponent<ApplyAttackModifiers>() != null) || (!item.addToProjectiles && gameObject.GetComponent<ApplyAttackModifiers>() == null))
            {
                MonoScript itemScript = item.itemScript;
                System.Type m_ScriptClass = itemScript.GetClass();
                if (gameObject.GetComponent(m_ScriptClass) == null)
                {
                    Component newComponent = gameObject.AddComponent(m_ScriptClass);
                    ItemScript scriptToFunny = (ItemScript)newComponent;
                    Debug.Log("script thing: " + scriptToFunny.ToString());
                    itemScripts.Add(scriptToFunny);
                    scriptToFunny.objectsToUse = item.objectsUsedByItem;
                    scriptToFunny.abilitiesToUse = item.abilitiesUsedByItem;
                }

                ((ItemScript)(gameObject.GetComponent(m_ScriptClass))).AddInstance();
            }
        }
        noToGive = 1;
    }

    void Start()
    {
        foreach (ItemSOInst item in itemsHeldTransferred)
        {
            ApplyItem(item);
            Debug.Log("application of item");
        }
    }

    public void OnHits(GameObject victim, GameObject responsible)
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnHit(victim, responsible);
        }
    }
    
    public void OnWallHits(GameObject victim, GameObject responsible)
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnWallHit(victim, responsible);
        }
    }

    public void OnPrimaryUses()
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnPrimaryUse();
        }
    }
    
    public void OnAbilityUses(int abilityIndex)
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnAbilityUse(abilityIndex);
        }
    }
    
    public void OnDodgeEnds()
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnDodgeEnd();
        }
    }

    public void OnKills(GameObject victim)
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnKill(victim);
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
    
    public void OnXPPickups()
    {
        foreach (ItemScript item in itemScripts)
        {
            item.OnXPPickup();
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
