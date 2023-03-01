using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabPlayer : MonoBehaviour
{
    GameObject blumby;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("lele");

            blumby = col.gameObject;
            blumby.GetComponent<NewPlayerMovement>().slowTimer = -1;
            blumby.GetComponent<NewPlayerMovement>().speedDiv = 999999;
            
            gameObject.GetComponent<NewPlayerMovement>().slowTimer = -1;
            gameObject.GetComponent<NewPlayerMovement>().speedDiv = 999999;
            gameObject.GetComponent<NewPlayerMovement>().recievesKnockback = false;
        }
    }

    void ApplyOwnOnDeaths()
    {
        blumby.GetComponent<NewPlayerMovement>().slowTimer = 1;
    }
}
