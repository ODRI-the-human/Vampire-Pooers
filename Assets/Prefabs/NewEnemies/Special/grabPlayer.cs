using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabPlayer : MonoBehaviour
{
    GameObject blumby;
    Vector3 playerPos = Vector3.zero;
    
    void Update()
    {
        if (blumby != null && Vector3Int.RoundToInt(playerPos / 3) != Vector3Int.RoundToInt(blumby.transform.position / 3))
        {
            gameObject.GetComponent<NewPlayerMovement>().slowTimer = -1;
            gameObject.GetComponent<NewPlayerMovement>().speedDiv = 1;
            gameObject.GetComponent<NewPlayerMovement>().recievesKnockback = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("lele");

            blumby = col.gameObject;
            blumby.GetComponent<NewPlayerMovement>().slowTimer = -1;
            blumby.GetComponent<NewPlayerMovement>().speedDiv = 999999;
            playerPos = blumby.transform.position;
            
            gameObject.GetComponent<NewPlayerMovement>().slowTimer = -1;
            gameObject.GetComponent<NewPlayerMovement>().speedDiv = 999999;
            gameObject.GetComponent<NewPlayerMovement>().recievesKnockback = false;
        }
    }

    void ApplyOwnOnDeaths()
    {
        if (blumby != null)
        {
            blumby.GetComponent<NewPlayerMovement>().slowTimer = 1;
        }
    }
}
