using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHOMING : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject closest;
    GameObject[] gos;
    bool isBullet = false;
    public int instances = 1;
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
            speed = rb.velocity.magnitude;
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

    void FindNewTarget()
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
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
    }

    void FixedUpdate()
    {
        if (isBullet)
        {
            if (closest == null)
            {
                FindNewTarget();
            }

            if ((transform.position - closest.transform.position).magnitude < 5 * instances && closest != null)
            {
                Vector2 closestEnemyPos = new Vector2(closest.transform.position.x, closest.transform.position.y);
                Vector2 bulletPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Vector2 vectorToEnemy = (closestEnemyPos - bulletPos).normalized;
                rb.velocity = speed * (rb.velocity + vectorToEnemy.normalized).normalized;
            }
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
