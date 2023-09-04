using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
    public int instances = 0; // the number of instances of the item held.
    public GameObject[] objectsToUse;
    public AbilityParams[] abilitiesToUse;

    public virtual void AddInstance() // applies any stat changes n shit that the item needs to do; for HP, fire rate ups, and such
    {
        instances++;
        AddStack();
    }

    public virtual void AddStack()
    {

    }

    public virtual void RemoveInstance()
    {
        instances--;
        if (instances == 0)
        {
            Destroy(this);
        }
        RemoveStack();
    }

    public virtual void RemoveStack()
    {

    }

    public virtual void OnHit(GameObject victim, GameObject responsible)
    {

    }
    
    public virtual void OnWallHit(GameObject victim, GameObject responsible)
    {

    }
    
    public virtual void OnPrimaryUse()
    {

    }
    
    public virtual void OnAbilityUse(int abilityIndex)
    {

    }
    
    public virtual void OnDodgeEnd()
    {

    }

    public virtual void OnKill(GameObject victim)
    {

    }

    public virtual void OnHurt()
    {

    }

    public virtual void OnLevel()
    {

    }
    
    public virtual void OnXPPickup()
    {

    }

    public virtual float DamageMult()
    {
        return 1f;
    }
}
