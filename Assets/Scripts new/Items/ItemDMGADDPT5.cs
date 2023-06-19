using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDMGADDPT5 : MonoBehaviour
{

    public int instances = 1;
    public float initialDamage;
    public float bonusDamage;


    // Start is called before the first frame update
    void Awake()
    {
        //GetDamVal();
    }

    void GetDamageMods()
    {
        gameObject.GetComponent<DealDamage>().damageToPassToVictim *= 1 + instances * 0.25f;
    }

    //void GetDamVal()
    //{
    //    initialDamage = gameObject.GetComponent<DealDamage>().damageBase;
    //    bonusDamage = 1 + 0.25f * instances;
    //    gameObject.GetComponent<DealDamage>().damageBase*= bonusDamage;
    //}

    //void ResetVal()
    //{
    //    gameObject.GetComponent<DealDamage>().damageBase /= bonusDamage;
    //}

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            //Invoke(nameof(ResetVal), 0.005f);
            instances++;
            //Invoke(nameof(GetDamVal), 0.005f);
        }
    }

    public void Undo()
    {
        //ResetVal();
        Destroy(this);
    }
}
