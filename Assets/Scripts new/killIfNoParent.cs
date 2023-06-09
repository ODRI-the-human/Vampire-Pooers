using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killIfNoParent : MonoBehaviour
{
    void Update()
    {
        if (transform.parent.gameObject == null)
        {
            Destroy(gameObject);
        }
    }
}
