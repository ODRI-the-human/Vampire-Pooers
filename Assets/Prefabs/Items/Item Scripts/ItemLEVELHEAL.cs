using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLEVELHEAL : ItemScript
{
    public override void AddStack()
    {
        gameObject.GetComponent<LevelUp>().healMult += 1;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<LevelUp>().healMult -= 1;
    }
}
