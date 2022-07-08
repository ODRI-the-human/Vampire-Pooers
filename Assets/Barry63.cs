using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barry63 : MonoBehaviour
{

    int time = 0;
    float time2;
    Vector3 scaleChange;

    // Update is called once per frame
    void Update()
    {
        time2 = 0.02f*time + Mathf.Sin(0.01234f*time);
        scaleChange = new Vector3((1.5f+Mathf.Sin(time2)), (1.5f+Mathf.Cos(time2)), 0);
        transform.localScale = scaleChange;
        transform.rotation = new Quaternion(0f, 0.01f*time, 0f, 0f);
        time += 3;
    }
}
