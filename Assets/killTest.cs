using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killTest : MonoBehaviour
{
    void OnCollisionEnter2D(Collider2D collider)
    {
        Debug.Log("lol, lmao, lmao, xd, lol");
        Destroy(gameObject);
    }
}
