using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMovement : MonoBehaviour
{
    Vector2 vectorToEnemy;
    public GameObject Player;
    public GameObject ATGMissile;
    Vector2 closestEnemyPos;
    Vector2 mousePos;
    Vector2 bulletPos;
    Vector3 currentNearest;
    float ATGProc;
    private int Timer = 0;
    int homingInstances = 0;
    int ATGInstances = 0;
    List<int> Sploinky = new List<int>();


    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    void Start()
    {
        Sploinky = FindObjectOfType<Player_Movement>().itemsHeld;
        foreach (int item in Sploinky)
        {
            //Debug.Log(item.ToString());
            switch (item)
            {
                case (int)ITEMLIST.HOMING:
                    homingInstances++;
                    break;
                case (int)ITEMLIST.ATG:
                    ATGInstances++;
                    break;
            }
        }
    }

    void Update()
    {
        Timer += 1;
        if (Timer > 1500)
        {
            Destroy(gameObject);
        }
    }

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

        if (homingInstances >= 1)
        {
            if (distance < 5*homingInstances)
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (ATGInstances > 0) 
        {
                ATGProc = Random.Range(0, 10);
                if (ATGProc > (8-0.5*ATGInstances))
                {
                    GameObject[] gos;
                    gos = GameObject.FindGameObjectsWithTag("Player");
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
                Instantiate(ATGMissile, currentNearest, new Quaternion(1,0,0,0));
                }
        }
        Destroy(gameObject);
    }
}
