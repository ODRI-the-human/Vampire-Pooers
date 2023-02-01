using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ownerDestroy : MonoBehaviour
{
    public GameObject owner;

    // Update is called once per frame
    void Update()
    {
        if (owner == null)
        {
            Destroy(gameObject);
        }
    }
}
