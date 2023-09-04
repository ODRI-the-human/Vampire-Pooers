using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPERFECTHEAL : ItemScript
{
    int totalIncrease = 0;
    public List<int> amtIncreasesByStack = new List<int>();

    public override void OnLevel()
    {
        if (gameObject.GetComponent<HPDamageDie>().perfectWaves > 1)
        {
            gameObject.GetComponent<HPDamageDie>().MaxHP += 10 * instances;
            if (amtIncreasesByStack.Count != instances)
            {
                amtIncreasesByStack.Add(0);
            }
            amtIncreasesByStack[instances - 1] += 10 * instances;
            float healQuantity = gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP;
            gameObject.GetComponent<Healing>().Healo(healQuantity);
        }
    }
}
