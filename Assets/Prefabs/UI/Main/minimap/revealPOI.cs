using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revealPOI : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
