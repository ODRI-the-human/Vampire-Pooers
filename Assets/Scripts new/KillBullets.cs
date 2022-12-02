using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBullets : MonoBehaviour
{
    public int timeTillDelete = 100;
    int timer = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if (timer == timeTillDelete)
        {
            Destroy(gameObject);
        }
    }
}
