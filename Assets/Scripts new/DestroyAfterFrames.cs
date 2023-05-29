using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterFrames : MonoBehaviour
{
    public int timer = 25;

    // Update is called once per frame
    void Update()
    {
        timer--;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
