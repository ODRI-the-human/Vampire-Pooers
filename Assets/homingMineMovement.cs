using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingMineMovement : MonoBehaviour
{
    GameObject target;
    float HP = 100;
    GameObject[] gos;
    public Vector3 currVector;

    float xRotFac;
    float yRotFac;
    float zRotFac;

    float speed;

    public GameObject splosoin;

    void Start()
    {
        GetTarget();

        xRotFac = Random.Range(-1.1f, 1.1f);
        yRotFac = Random.Range(-1.1f, 1.1f);
        zRotFac = Random.Range(-1.1f, 1.1f);

        transform.position += new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0);

        speed = 8f;
    }

    void GetTarget()
    {
        if (gameObject.tag == "PlayerBullet")
        {
            gos = GameObject.FindGameObjectsWithTag("Hostile");
        }
        if (gameObject.tag == "enemyBullet")
        {
            gos = GameObject.FindGameObjectsWithTag("Player");
        }
        target = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                target = go;
                distance = curDistance;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currVector = 0.75f * speed * (10 * currVector.normalized + (target.transform.position - transform.position).normalized).normalized;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        transform.rotation *= Quaternion.Euler(xRotFac, yRotFac, zRotFac);
    }

    void Update()
    {
        transform.position += currVector * Time.deltaTime;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        transform.rotation *= Quaternion.Euler(xRotFac, yRotFac, zRotFac);
    }

    public void ApplyOwnOnDeaths()
    {
        GameObject splod = Instantiate(splosoin, transform.position, Quaternion.Euler(0, 0, 0));
        splod.GetComponent<DealDamage>().finalDamageStat = 3 * gameObject.GetComponent<DealDamage>().GetDamageAmount();
        splod.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Hostile")
        {
            ApplyOwnOnDeaths();
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ATGExplosion")
        {
            ApplyOwnOnDeaths();
        }
    }
}
