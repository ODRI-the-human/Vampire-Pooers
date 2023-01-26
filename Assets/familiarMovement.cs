using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class familiarMovement : MonoBehaviour
{

    public GameObject toFollow;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if (toFollow == null)
        {
            Destroy(gameObject);
        }

        transform.position += 2 * Time.deltaTime * (toFollow.transform.position - gameObject.transform.position);
    }
}
