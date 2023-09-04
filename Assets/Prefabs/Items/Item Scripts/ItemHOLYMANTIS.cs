using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOLYMANTIS : ItemScript
{
    public override void AddStack()
    {
        gameObject.GetComponent<HPDamageDie>().damageDiv = (float)(instances + 1);
        Debug.Log("erm that just happened, instances: " + instances.ToString());
    }

    public override void OnHurt()
    {
        if (gameObject.GetComponent<HPDamageDie>().damageDiv > 1)
        {
            gameObject.GetComponent<HPDamageDie>().damageDiv--;
        }
    }

    public override void OnLevel()
    {
        gameObject.GetComponent<HPDamageDie>().damageDiv = (float)(instances + 1);
    }
}
