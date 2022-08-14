using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOLYMANTIS : MonoBehaviour
{
    public float maxTimesHit = 1;
    public float timesHit;
    public int instances;
    float iFrames = 0;

    void Start()
    {
        instances = 1;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            timesHit = maxTimesHit;
        }
    }
}