using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableHitbox : MonoBehaviour
{
    int timer = 0;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer == 5)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }
        timer++;
    }
}
