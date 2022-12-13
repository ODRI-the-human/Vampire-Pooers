using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotation : MonoBehaviour
{
    public int mode = 0;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0, 2);
    }
}
