using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBETTERLEVEL : ItemScript
{
    // Start is called before the first frame update
    public override void AddStack()
    {
        gameObject.GetComponent<LevelUp>().effectMult += 1;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<LevelUp>().effectMult -= 1;
    }
}
