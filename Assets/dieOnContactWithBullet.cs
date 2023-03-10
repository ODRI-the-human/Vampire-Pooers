using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieOnContactWithBullet : MonoBehaviour
{
    public GameObject master;
    public bool calcScaleAuto;
    public int instances;

    public bool reduceInstOnHit = true;

    void Start()
    {
        if (calcScaleAuto)
        {
            transform.localScale = master.transform.localScale;
        }
    }

    void FixedUpdate()
    {
        if (master == null)
        {
            Destroy(gameObject);
        }

        transform.position = master.transform.position;
    }

    public void CommitDie()
    {
        Destroy(master);
        Destroy(gameObject);
    }
}
