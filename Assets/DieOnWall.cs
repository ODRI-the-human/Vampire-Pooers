using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnWall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            //Destroy(gameObject);
        }
    }
}
