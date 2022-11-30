using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTracking : MonoBehaviour
{
    public int instances;

    Vector2 currentNearest;
    Vector2 closestEnemyPos;
    Vector2 bulletPos;
    public Rigidbody2D rb;
    public GameObject explosion;
    public GameObject owner;

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject[] gos;
        if (gameObject.tag == "PlayerBullet")
        {
            gos = GameObject.FindGameObjectsWithTag("Hostile");
        }
        else
        {
            gos = GameObject.FindGameObjectsWithTag("Player");
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                currentNearest = go.transform.position;
            }
        }

        closestEnemyPos.x = currentNearest.x;
        closestEnemyPos.y = currentNearest.y;
        bulletPos.x = gameObject.transform.position.x;
        bulletPos.y = gameObject.transform.position.y;
        rb.velocity += (closestEnemyPos - bulletPos).normalized;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        GameObject newObject = Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        newObject.transform.localScale = new Vector3(2, 2, 2);
        newObject.GetComponent<DealDamage>().owner = owner;
        newObject.GetComponent<DealDamage>().damageAmt = 3 * instances * owner.GetComponent<DealDamage>().finalDamageStat;
        newObject.GetComponent<DealDamage>().knockBackCoeff = 2 * owner.GetComponent<DealDamage>().knockBackCoeff;
        Destroy(gameObject);
    }
}
