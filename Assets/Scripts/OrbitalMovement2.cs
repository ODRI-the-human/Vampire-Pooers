using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement2 : MonoBehaviour
{
    int timer = 0;
    GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("newPlayer");
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x + 0.8f * Mathf.Sin(0.08f * timer), Player.transform.position.y + 0.8f * Mathf.Cos(0.08f * timer), Player.transform.position.z);
        timer++;
    }
}
