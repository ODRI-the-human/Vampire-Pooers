using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBLEED : MonoBehaviour
{
    public int instances = 1;
    float pringle;
    float procMoment;
    float iFrameLength = 0;

    void RollOnHit(GameObject guyToEffect)
    {
        procMoment = 100f - instances * 15 * gameObject.GetComponent<DealDamage>().procCoeff;
        pringle = Random.Range(0f, 100f);
        if (pringle > procMoment)
        {
            guyToEffect.GetComponent<Statuses>().bleedStacks += 1;
            guyToEffect.GetComponent<Statuses>().bleedTimer = 0;
            if (!guyToEffect.GetComponent<Statuses>().iconOrder.Contains(0))
            {
                guyToEffect.GetComponent<Statuses>().iconOrder.Add(0);
            }
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
