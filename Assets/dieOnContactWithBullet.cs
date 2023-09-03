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

        gameObject.tag = master.tag;
        if (gameObject.tag == "enemyBullet")
        {
            int LayerEnemy = LayerMask.NameToLayer("HitPlayerBulletsAndPlayer");
            gameObject.layer = LayerEnemy;
        }
    }

    public void Kill()
    {
        Destroy(master);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position = master.transform.position;

        if (master == null)
        {
            Destroy(gameObject);
        }
    }
}
