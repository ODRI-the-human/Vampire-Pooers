using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestForceField : MonoBehaviour
{
    public float radius = 5f;
    public float strength = 0.3f;

    void FixedUpdate()
    {
        Collider2D[] jimbob = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var col in jimbob)
        {
            if (col.gameObject.tag == "enemyBullet")
            {
                Vector3 gromble = transform.position - col.gameObject.transform.position;
                Vector2 grombley = 0.4f * new Vector2(gromble.x, gromble.y).normalized;
                col.gameObject.GetComponent<Rigidbody2D>().velocity -= grombley;
            }

            if (col.gameObject.tag == "Hostile")
            {
                col.gameObject.GetComponent<NewPlayerMovement>().knockBackVector += strength * new Vector2((col.transform.position - transform.position).x, (col.transform.position - transform.position).y);
            }
        }
    }
}
