using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public float healMult = 1;
    public float healDiv = 1;
    GameObject master;

    void Start()
    {
        master = EntityReferencerGuy.Instance.master;
    }

    public void newWaveEffects()
    {
        Healo(10);
    }

    public void Healo(float amount)
    {
        float healyAmount = amount * healMult / healDiv;
        if (gameObject.GetComponent<HPDamageDie>().HP + healyAmount > gameObject.GetComponent<HPDamageDie>().MaxHP) // If player would overheal, reduces healing amount to what's required to reach max HP.
        {
            //Debug.Log(healyAmount.ToString());
            healyAmount = gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP;
        }
        gameObject.GetComponent<HPDamageDie>().HP += healyAmount;

        if (healyAmount > 0)
        {
            master.GetComponent<showDamageNumbers>().showDamage(transform.position, amount * healMult / healDiv, (int)DAMAGETYPES.HEAL, false);
        }
    }
}
