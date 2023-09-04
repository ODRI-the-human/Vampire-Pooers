using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOY : ItemScript
{
    public override void AddStack()
    {
        gameObject.GetComponent<Attack>().cooldownFac /= 2f;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<Attack>().cooldownFac *= 2f;
    }

    public override float DamageMult()
    {
        float damageMult = 1;
        damageMult /= 2 * instances;
        return damageMult;
    }
}
