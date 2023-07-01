using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnHit : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<HPDamageDie>() != null) // only heals player if object has an HPDamageDie (so only enemies)
        {
            gameObject.GetComponent<DealDamage>().owner.GetComponent<Healing>().Healo(5);
        }
    }
}
