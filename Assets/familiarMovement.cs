using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class familiarMovement : MonoBehaviour
{

    public GameObject toFollow;
    public Rigidbody2D rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toFollow == null)
        {
            Destroy(gameObject);
        }

        rb.velocity = 2 * new Vector2(toFollow.transform.position.x - gameObject.transform.position.x, toFollow.transform.position.y - gameObject.transform.position.y);

        if ((toFollow.transform.position - gameObject.transform.position).magnitude > 10)
        {
            gameObject.transform.position = toFollow.transform.position;
        }
    }
}
