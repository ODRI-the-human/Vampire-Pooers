using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spin : MonoBehaviour
{
    public float constantRotation = 90;
    public float maxRotation = 360;

    private Rigidbody2D rob;

    void Start()
    {
        rob = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rob.angularVelocity < maxRotation)
            rob.AddTorque((constantRotation * Time.deltaTime) * rob.mass);
        else
            rob.angularVelocity = maxRotation;
    }
}
