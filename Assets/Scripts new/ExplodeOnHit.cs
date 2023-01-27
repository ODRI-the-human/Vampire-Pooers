using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeOnHit : MonoBehaviour
{
    public GameObject explosion;
    GameObject owner;
    float damageAmt;

    int timer = 0;
    
    void Start()
    {
        GameObject master = gameObject.GetComponent<DealDamage>().master;
        explosion = master.GetComponent<EntityReferencerGuy>().neutralExplosion;
    }

    void FixedUpdate()
    {
        if (timer == 15 && gameObject.GetComponent<Rigidbody2D>().simulated)
        {
            exploSoin();
            Destroy(gameObject);
        }

        timer++;

        gameObject.GetComponent<Rigidbody2D>().velocity /= 1.15f;
    }

    void exploSoin()
    {
        GameObject splodo = Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0));
        splodo.transform.localScale = new Vector3(2, 2, 2);
        splodo.GetComponent<DealDamage>().damageAmt = 3 * gameObject.GetComponent<DealDamage>().damageAmt;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        exploSoin();
    }
}
