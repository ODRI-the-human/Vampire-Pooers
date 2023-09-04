using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHP50 : ItemScript
{
    // Start is called before the first frame update
    public override void AddStack()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP += 50;
   
        gameObject.GetComponent<HPDamageDie>().HP += 50;
    }

    public override void RemoveStack()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP -= 50;
        gameObject.GetComponent<HPDamageDie>().HP = Mathf.Clamp(gameObject.GetComponent<HPDamageDie>().HP, 0, gameObject.GetComponent<HPDamageDie>().MaxHP);
    }
}
