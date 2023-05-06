using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDMGADDPT5 : MonoBehaviour
{

    public int instances = 1;
    public float initialDamage;
    public float bonusDamage;


    // Start is called before the first frame update
    void Start()
    {
        GetDamVal();
    }

    void GetDamVal()
    {
        initialDamage = gameObject.GetComponent<DealDamage>().damageBase;
        bonusDamage = instances * initialDamage / 4;
        gameObject.GetComponent<DealDamage>().damageBase += bonusDamage;
    }

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            gameObject.GetComponent<DealDamage>().damageBase += initialDamage / 4;
            bonusDamage += initialDamage / 4;
            instances++;
            //GetDamVal();
        }
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().damageBase -= bonusDamage;
        Destroy(this);
    }
}
