using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMOREXP : ItemScript
{
    public override void AddStack()
    {
        gameObject.GetComponent<LevelUp>().xpMult += 0.3f;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<LevelUp>().xpMult -= 0.3f;
    }
}
