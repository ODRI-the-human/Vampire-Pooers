using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCONVERTER : MonoBehaviour
{

    public int instances = 1;

    public void newWaveEffects()
    {
        gameObject.GetComponent<Attack>().Crongus += 10 * instances* ((gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP)/ gameObject.GetComponent<HPDamageDie>().MaxHP);
        gameObject.GetComponent<DealDamage>().damageBase += 10 * instances* ((gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP)/ gameObject.GetComponent<HPDamageDie>().MaxHP);
    }


    public void Undo()
    {
        gameObject.GetComponent<Attack>().Crongus = 0;
    }
}
