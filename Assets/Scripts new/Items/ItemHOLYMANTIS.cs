using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOLYMANTIS : MonoBehaviour
{
    public float maxTimesHit = 1;
    public float timesHit;
    public int instances = 1;
    float iFrames = 0;

    void Start()
    {
        maxTimesHit = instances;
        timesHit = maxTimesHit;
    }

    void FixedUpdate()
    {
        iFrames--;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != gameObject.tag && timesHit > 0)
        {
            if (iFrames < 0)
            {
                gameObject.GetComponent<HPDamageDie>().HP += instances * col.gameObject.GetComponent<DealDamage>().finalDamageStat / (instances + 1);
                iFrames = gameObject.GetComponent<HPDamageDie>().iFramesTimer;
                timesHit--;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != gameObject.tag && timesHit > 0 && col.gameObject.GetComponent<DealDamage>().finalDamageStat != 0)
        {
            if (iFrames < 0)
            {
                gameObject.GetComponent<HPDamageDie>().HP += instances * col.gameObject.GetComponent<DealDamage>().finalDamageStat / (instances + 1);
                iFrames = gameObject.GetComponent<HPDamageDie>().iFramesTimer;
                timesHit--;
            }
        }

        if (col.tag == "item")
        {
            timesHit = maxTimesHit;
        }
    }

    public void Undo()
    {
        //nothin
    }
}
