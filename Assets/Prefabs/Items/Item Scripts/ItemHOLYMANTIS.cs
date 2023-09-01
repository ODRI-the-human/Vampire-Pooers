using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOLYMANTIS : MonoBehaviour
{
    public int instances = 1;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        newWaveEffects();
    }

    public void OnHurtEffects()
    {
        if (gameObject.GetComponent<HPDamageDie>().damageDiv > 1)
        {
            gameObject.GetComponent<HPDamageDie>().damageDiv--;
        }
    }

    public void newWaveEffects()
    {
        gameObject.GetComponent<HPDamageDie>().damageDiv = (instances + 1);
    }

    public void Undo()
    {
        Destroy(this);
    }
}
