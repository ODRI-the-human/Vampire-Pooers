using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creep : MonoBehaviour
{
    public int destroyDelay = 5;

    void Start()
    {
        Invoke(nameof(DestorySelf), destroyDelay); //will invoke (run the function) in so many seconds
    }

    void DestorySelf()
    {
        Destroy(gameObject);
    }
}
