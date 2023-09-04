using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMORESHOT : ItemScript
{
    public override void AddStack()
    {
        gameObject.GetComponent<Attack>().noExtraShots++;
        gameObject.GetComponent<Attack>().cooldownFac *= 1.25f;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<Attack>().noExtraShots--;
        gameObject.GetComponent<Attack>().cooldownFac /= 1.25f;
    }
}
