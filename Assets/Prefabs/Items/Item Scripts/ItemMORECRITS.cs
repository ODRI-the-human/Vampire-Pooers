using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMORECRITS : ItemScript
{
    public override void AddStack()
    {
        gameObject.GetComponent<DealDamage>().critProb += 0.2f;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<DealDamage>().critProb -= 0.2f;
    }
}
