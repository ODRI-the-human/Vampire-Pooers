using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHEALTHXP : ItemScript
{
    public override void OnXPPickup()
    {
        gameObject.GetComponent<LevelUp>().XP += Mathf.RoundToInt(10 * 2 * instances * (gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP) / gameObject.GetComponent<HPDamageDie>().MaxHP);
    }
}
