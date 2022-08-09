using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnHit : MonoBehaviour
{
    public GameObject explosion;
    public Rigidbody2D rb;
    public GameObject explosionPlayerHit;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject splodo = Instantiate(explosion, transform.position, transform.rotation);
        splodo.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        GameObject boombo2 = Instantiate(explosionPlayerHit, transform.position, transform.rotation);
        boombo2.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
