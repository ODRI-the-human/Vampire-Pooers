using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnerSpin : MonoBehaviour
{
    int timer = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, timer);
        timer++;
    }
}
