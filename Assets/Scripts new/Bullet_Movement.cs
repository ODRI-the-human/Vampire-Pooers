using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    float speed;

    void Start()
    {
        speed = 5; //rb.velocity.magnitude
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if (gameObject.GetComponent<ItemBOUNCY>() == null)
        //{
            //Destroy(gameObject);
        //}
        //else
        //{
            //if (gameObject.GetComponent<ItemBOUNCY>().bouncesLeft > 0)
            //{
                Vector2 enemyPos = new Vector2 (transform.position.x, transform.position.y);
                Vector2 bulletPos = new Vector2 (col.transform.position.x, col.transform.position.y);
                rb.velocity = speed * (bulletPos - enemyPos).normalized;
                gameObject.GetComponent<ItemBOUNCY>().bouncesLeft--;
            //}
            //else
            //{
            //    Destroy(gameObject);
            //}
        //}
    }
}
