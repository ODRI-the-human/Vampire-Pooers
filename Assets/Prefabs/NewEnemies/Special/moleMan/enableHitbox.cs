using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableHitbox : MonoBehaviour
{
    public void enableHitboxer()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public void FUCKINGDIE()
    {
        Destroy(gameObject);
    }
}
