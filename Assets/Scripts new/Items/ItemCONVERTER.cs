using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCONVERTER : MonoBehaviour
{

    public int instances;

    void Start()
    {
        instances = 1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            gameObject.GetComponent<DealDamage>().damageBase += 10 * instances * ((gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP)/ gameObject.GetComponent<HPDamageDie>().MaxHP);
        }
    }
}