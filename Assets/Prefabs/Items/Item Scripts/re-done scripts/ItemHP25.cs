using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHP25 : ItemScript
{
    // Start is called before the first frame update
    public override void AddStack()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP += 25;
        gameObject.GetComponent<HPDamageDie>().HP += 25;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP -= 25;
        gameObject.GetComponent<HPDamageDie>().HP = Mathf.Clamp(gameObject.GetComponent<HPDamageDie>().HP, 0, gameObject.GetComponent<HPDamageDie>().MaxHP);
    }
}
