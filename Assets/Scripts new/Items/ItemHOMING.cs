using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOMING : MonoBehaviour
{
    int homingCheckTimer;
    Vector3 currentNearest;
    Vector2 closestEnemyPos;
    Vector2 bulletPos;
    Vector2 vectorToEnemy;
    Rigidbody2D rb;
    GameObject closest;
    bool isBullet = false;
    public int instances = 1;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "Bullet")
        {
            isBullet = true;
        }
    }

    void FixedUpdate()
    {
        homingCheckTimer--;
        if (homingCheckTimer <= 0 && isBullet)
        {
            homingCheckTimer = 6;
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Hostile");
            closest = null;
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
                }
            }
        }

        currentNearest = closest.transform.position;

        if ((transform.position - currentNearest).magnitude < 5 * instances)
        {
            closestEnemyPos.x = currentNearest.x;
            closestEnemyPos.y = currentNearest.y;
            bulletPos.x = gameObject.transform.position.x;
            bulletPos.y = gameObject.transform.position.y;
            vectorToEnemy = (closestEnemyPos - bulletPos).normalized;
            rb.velocity = 10f * (rb.velocity + vectorToEnemy.normalized).normalized;
        }
    }
}
