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

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        if (gameObject.GetComponent<checkAllLazerPositions>() == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            if (gameObject.GetComponent<Bullet_Movement>() != null)
            {
                isBullet = true;
            }
        }

        if (gameObject.GetComponent<meleeGeneral>() != null) // For applying melee weapons' unique homing
        {
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
                float angle = Vector3.Angle(diff, transform.right);
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    if (angle < 60) // checks if enemy is within some number of degrees of this object's direction, if so, count it as a guy.
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
            }

            if (closest != null)//((closest.transform.position - transform.position).magnitude + gameObject.GetComponent<meleeGeneral>().maxDist > 5f * instances))
            {
                Vector3 overallVec = closest.transform.position - transform.position;
                //overallVec = overallVec - overallVec.normalized * gameObject.GetComponent<meleeGeneral>().maxDist / 2;
                float dist = overallVec.magnitude;
                if (dist > gameObject.GetComponent<meleeGeneral>().maxDist)
                {
                    if (dist + gameObject.GetComponent<meleeGeneral>().maxDist > 2 * instances)
                    {
                        transform.position += overallVec.normalized * 2 * instances;
                    }
                    else
                    {
                        transform.position += closest.transform.position - transform.position;
                    }
                }
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

                homingCheckTimer = 25;
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
