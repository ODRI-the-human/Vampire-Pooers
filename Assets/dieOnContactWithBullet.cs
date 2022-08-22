using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieOnContactWithBullet : MonoBehaviour
{
    public GameObject master;
    public int instances;

    void Start()
    {
        transform.localScale = master.transform.localScale;
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
