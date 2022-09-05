using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBLEED : MonoBehaviour
{
    GameObject guyToBleed;
    public int instances = 1;
    float pringle;
    float procMoment;

    void OnTriggerEnter2D(Collider2D col)
    {
        guyToBleed = col.gameObject;
        DoABleed();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        guyToBleed = col.gameObject;
        DoABleed();
    }

    void DoABleed()
    {
        if (guyToBleed.GetComponent<HPDamageDie>().iFrames <= 0)
        {
            procMoment = 100f - instances * 15 * gameObject.GetComponent<DealDamage>().procCoeff;
            pringle = Random.Range(0f, 100f);

            if (pringle > procMoment)
            {
                if (guyToBleed.GetComponent<Bleed>() == null)
                {
                    guyToBleed.AddComponent<Bleed>();
                }
                else
                {
                    guyToBleed.GetComponent<Bleed>().stacks += 1;
                    guyToBleed.GetComponent<Bleed>().timer = 0;
                }
            }
        }
    }
}
