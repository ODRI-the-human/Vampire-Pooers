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
        AddStack();
        instances++;
    }

    public virtual void AddStack()
    {

    }

    public virtual void RemoveInstance()
    {
        RemoveStack();
        instances--;
        if (instances == 0)
        {
            Destroy(this);
        }
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
    
    public virtual void OnAbilityUse()
    {

    }
    
    public virtual void OnDodgeEnd()
    {

    }

    public virtual void OnKill()
    {

    }

    public virtual void OnHurt()
    {

    }

    public virtual void OnLevel()
    {

    }

    public virtual float DamageMult()
    {
        return 1f;
    }
}
