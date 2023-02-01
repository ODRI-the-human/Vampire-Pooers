using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingMineMovement : MonoBehaviour
{
    GameObject target;
    float HP = 100;

    float xRotFac;
    float yRotFac;
    float zRotFac;

    float speed;

    public GameObject splosoin;

    void Start()
    {
        GameObject misterFart = gameObject.GetComponent<DealDamage>().owner;
        target = misterFart.GetComponent<Attack>().currentTarget;

        xRotFac = Random.Range(-1.1f, 1.1f);
        yRotFac = Random.Range(-1.1f, 1.1f);
        zRotFac = Random.Range(-1.1f, 1.1f);

        speed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += 0.75f * speed * Time.deltaTime * (target.transform.position - transform.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        transform.rotation *= Quaternion.Euler(xRotFac, yRotFac, zRotFac);
    }

    public void ApplyOwnOnDeaths()
    {
        GameObject splod = Instantiate(splosoin, transform.position, Quaternion.Euler(0, 0, 0));
        splod.transform.localScale /= 1.5f;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
