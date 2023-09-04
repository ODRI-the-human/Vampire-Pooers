using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHEALMLT : ItemScript
{
    // Start is called before the first frame update
    public override void AddStack()
    {
        gameObject.GetComponent<Healing>().healMult += 1;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<Healing>().healMult -= 1;
    }
}
