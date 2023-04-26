using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPERFECTHEAL : MonoBehaviour
{
    public int instances = 1;
    int totalIncrease = 0;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void newWaveEffects()
    {
        if (gameObject.GetComponent<HPDamageDie>().perfectWaves > 1)
        {
            gameObject.GetComponent<HPDamageDie>().MaxHP += 10 * instances;
            totalIncrease += 10 * instances;
            float healQuantity = gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP;
            gameObject.GetComponent<Healing>().Healo(healQuantity);
        }
    }

    public void Undo()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP -= totalIncrease;
        Destroy(this);
    }
}
