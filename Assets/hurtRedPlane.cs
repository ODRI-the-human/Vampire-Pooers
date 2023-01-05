using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtRedPlane : MonoBehaviour
{
    Color bumHead;

    void Start()
    {
        bumHead.a = 0.05f;
    }


    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color - bumHead;
    }
}
