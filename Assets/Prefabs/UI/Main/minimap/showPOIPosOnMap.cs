using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPOIPosOnMap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
