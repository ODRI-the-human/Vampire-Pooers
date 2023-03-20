using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterPositioning : MonoBehaviour
{
    public Vector3 properPos;
    public bool isEvil = false;

    // Start is called before the first frame update
    void Start()
    {
        properPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEvil)
        {
            float shakeAmt = 0.06f;
            transform.position = properPos + new Vector3(Random.Range(-shakeAmt, shakeAmt), Random.Range(-shakeAmt, shakeAmt), 0);
        }
    }
}
