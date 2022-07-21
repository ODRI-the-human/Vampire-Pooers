using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerShrink2 : MonoBehaviour
{

    int timer = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;

        if (timer > 0)
        {
            Destroy(gameObject);
        }
    }
}
