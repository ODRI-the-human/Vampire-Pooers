using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCONVERTER : ItemScript
{
    public override void OnLevel()
    {
        //gameObject.GetComponent<Attack>().Crongus += Mathf.Clamp(10 * instances* ((gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP)/ gameObject.GetComponent<HPDamageDie>().MaxHP),0, 999999);
        gameObject.GetComponent<DealDamage>().damageBonus += 0.1f * instances * ((gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP)/ gameObject.GetComponent<HPDamageDie>().MaxHP);
    }
}
