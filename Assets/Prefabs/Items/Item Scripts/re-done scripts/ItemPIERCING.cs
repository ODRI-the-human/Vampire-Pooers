using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPIERCING : ItemScript
{
    float damBonus = 1;

    public void IncreaseDamageBonus()
    {
        Invoke(nameof(GiveMoreDamage), 0.01f);
    }

    void GiveMoreDamage()
    {
        damBonus += 0.2f * instances;
    }

    public override float DamageMult()
    {
        return damBonus;
    }

    void Start()
    {
        damBonus = 1;
        if (gameObject.GetComponent<Bullet_Movement>() != null)
        {
            gameObject.GetComponent<Bullet_Movement>().piercesLeft += 1;
        }
    }
}
