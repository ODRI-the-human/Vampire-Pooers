using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Vector3 mousePos3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 blodsfofd = new Vector2(mousePos3.x, mousePos3.y).normalized;
        transform.position = blodsfofd + Player.transform.position;
        //if (Player.GetComponent<Player_Movement>().weaponHeld != 3)
        //{
        //    Destroy(gameObject);
        //}
    }
}
