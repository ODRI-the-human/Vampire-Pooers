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
    GameObject[] gos;
    public float speed;

    void Start()
    {
        if (gameObject.GetComponent<checkAllLazerPositions>() == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
            {
                isBullet = true;
            }
        }
    }

    void Update()
    {
        if (isBullet)
        {
            GameObject bumbo = gameObject.GetComponent<DealDamage>().owner;
            speed = bumbo.GetComponent<Attack>().shotSpeed;
        }
    }

    void FixedUpdate()
    {
        if (isBullet)
        {
            homingCheckTimer--;
            if (homingCheckTimer <= 0)
            {
                currentNearest = Vector3.zero;

                homingCheckTimer = 6;
                if (gameObject.tag == "PlayerBullet")
                {
                    gos = GameObject.FindGameObjectsWithTag("Hostile");
                }
                if (gameObject.tag == "enemyBullet")
                {
                    gos = GameObject.FindGameObjectsWithTag("Player");
                }
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

            if (closest != null)
            {
                currentNearest = closest.transform.position;
            }

            if ((transform.position - currentNearest).magnitude < 5 * instances && currentNearest != Vector3.zero)
            {
                closestEnemyPos.x = currentNearest.x;
                closestEnemyPos.y = currentNearest.y;
                bulletPos.x = gameObject.transform.position.x;
                bulletPos.y = gameObject.transform.position.y;
                vectorToEnemy = (closestEnemyPos - bulletPos).normalized;
                rb.velocity = speed * (rb.velocity + vectorToEnemy.normalized).normalized;
            }
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
