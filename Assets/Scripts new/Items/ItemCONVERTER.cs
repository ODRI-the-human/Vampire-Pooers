using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCONVERTER : MonoBehaviour
{

    public int instances = 1;

    public void newWaveEffects()
    {
        gameObject.GetComponent<Attack>().Crongus += Mathf.Clamp(10 * instances* ((gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP)/ gameObject.GetComponent<HPDamageDie>().MaxHP),0, 999999);
        gameObject.GetComponent<DealDamage>().damageBase += 10 * instances* ((gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP)/ gameObject.GetComponent<HPDamageDie>().MaxHP);
    }


    public void Undo()
    {
        Destroy(this);
    }
}
