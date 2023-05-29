using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieIfOwnerIsNull : MonoBehaviour
{
    public GameObject owner;

    void Update()
    {
        if (owner == null)
        {
            Destroy(gameObject);
        }
    }
}
