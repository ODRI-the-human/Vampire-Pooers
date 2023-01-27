using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeOnHit : MonoBehaviour
{
    public GameObject explosion;
    GameObject owner;
    float damageAmt;
    
    void Start()
    {
        GameObject master = gameObject.GetComponent<DealDamage>().master;
        explosion = master.GetComponent<EntityReferencerGuy>().neutralExplosion;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject splodo = Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0));
        splodo.transform.localScale = new Vector3(2, 2, 2);
        splodo.GetComponent<DealDamage>().damageAmt = 2 * gameObject.GetComponent<DealDamage>().damageAmt;
    }
}
