using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLUCKIER : ItemScript
{
    public override void AddStack()
    {
        float increaseRate = 5; // Increase this to reduce the rate at which this item scales.
        float bonusAmt = 2; // Increase this to make the item give you a larger bonus (and higher asymptote)
        gameObject.GetComponent<DealDamage>().procChanceBonus = Mathf.Pow(Mathf.Log(instances + 1) + 1, 1.3f);
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<DealDamage>().procChanceBonus = Mathf.Pow(Mathf.Log(instances + 1) + 1, 1.3f);
    }
}
