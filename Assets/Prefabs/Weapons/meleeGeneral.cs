using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeGeneral : MonoBehaviour
{
    public float maxDist; //Max dist is the approximate distance between the furthest edge of the attack hitbox and the user. Important for homing n shit.
    public int weapon; //Which weapon is this? 0 = bat, 1 = dark arts
    public bool isCharged; //Is the attack charged?

    void Start()
    {
        Invoke(nameof(EnableCollision), 0.01f);
    }

    void EnableCollision()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
