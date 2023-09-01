using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOY : ItemScript
{
    public override void AddInstance()
    {
        gameObject.GetComponent<Attack>().cooldownFac /= 2f;
        instances++;
    }

    public override void RemoveInstance()
    {
        gameObject.GetComponent<Attack>().cooldownFac *= 2f;
        instances--;
        if (instances == 0)
        {
            Destroy(this);
        }
    }

    public override void OnHit(GameObject victim, GameObject responsible)
    {

    }

    public override void OnKill()
    {

    }

    public override void OnHurt()
    {

    }
    
    public override void OnLevel()
    {

    }

    public override float DamageMult()
    {
        float damageMult = 1;
        damageMult /= 2 * instances;
        Debug.Log("damage got multiplied - what a cool moment: " + damageMult.ToString());
        return damageMult;
    }
}
