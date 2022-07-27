using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterABitOk : MonoBehaviour
{

    public int timer = 25;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= 1;
        if (timer <= 0)
            {
            Destroy(gameObject);
            }
    }
}
