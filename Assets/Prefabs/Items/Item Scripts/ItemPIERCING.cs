using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPIERCING : MonoBehaviour
{
    public int instances = 1;
    float damBonus = 1;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    public void IncreaseDamageBonus()
    {
        Invoke(nameof(DoDamageBuff), 0.01f); // Just to make it so the damage buff gets applied specifically after the collision occurs.
    }

    void DoDamageBuff()
    {
        damBonus += 0.2f * instances;
    }

    void GetDamageMods()
    {
        gameObject.GetComponent<DealDamage>().damageToPassToVictim *= damBonus;
    }

    void Start()
    {
        DetermineShotRolls();
    }

    void DetermineShotRolls()
    {
        damBonus = 1;
        if (gameObject.GetComponent<Bullet_Movement>() != null)
        {
            gameObject.GetComponent<Bullet_Movement>().piercesLeft += 1;
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
