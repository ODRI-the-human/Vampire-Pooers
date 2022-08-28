using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wapantCircle : MonoBehaviour
{
    public int destroyDelay = 8;
    float maxTimer = 100;
    float timer = 100;
    GameObject Player;

    void Start()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        Player = gos[0];
        Invoke(nameof(DestorySelf), destroyDelay); //will invoke (run the function) in so many seconds
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer /= 1.01f;
        transform.localScale += 0.6f * new Vector3(0.3f*timer / maxTimer, 0.3f* timer / maxTimer, 0);
    }

    void DestorySelf()
    {
        Destroy(gameObject);
    }
}
