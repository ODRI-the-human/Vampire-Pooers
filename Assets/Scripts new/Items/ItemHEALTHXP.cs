using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHEALTHXP : MonoBehaviour
{
    public int instances = 1;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "XP")
        {
            gameObject.GetComponent<LevelUp>().XP += Mathf.RoundToInt(10 * 2 * instances * (gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP) / gameObject.GetComponent<HPDamageDie>().MaxHP);
        }
    }

    void Undo()
    {
        Destroy(this);
    }
}
