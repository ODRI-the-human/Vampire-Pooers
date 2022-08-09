using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTracking : MonoBehaviour
{
    Vector2 currentNearest;
    Vector2 closestEnemyPos;
    Vector2 bulletPos;
    public Rigidbody2D rb;
    public GameObject explosion;

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Hostile");
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
        GameObject newObject = Instantiate(explosion, transform.position, new Quaternion(1,0,0,0)) as GameObject;
        newObject.transform.localScale = new Vector3(1, 1, 1);
        Destroy(gameObject);
    }
}
