using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    int timer = 0;
    float timerVariance;
    GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("Player");
        timerVariance = Random.Range(0.6f, 1.4f);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x + 2.3f * Mathf.Sin(0.035f * timer * timerVariance), Player.transform.position.y + 2.3f * Mathf.Cos(0.035f * timer * timerVariance), Player.transform.position.z);
        timer++;
    }
}
