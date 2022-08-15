using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterMarty : MonoBehaviour
{

    public int timer = 25;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
