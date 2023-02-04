using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableHitbox : MonoBehaviour
{
    public void enableHitboxer()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
