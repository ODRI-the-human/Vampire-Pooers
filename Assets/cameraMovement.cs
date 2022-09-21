using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    GameObject Player;
    public float amountToChangeWithMouse = 0.05f;
    public float moveSpeed = 0.04f;

    float xBound = 18.5f;
    float yBound = 10.7f;

    void Start()
    {
        Player = GameObject.Find("newPlayer");
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 distFromPlayer = Player.transform.position - transform.position + amountToChangeWithMouse * new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y, 0).normalized;
        distFromPlayer.z = 0;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10.6f) + moveSpeed * distFromPlayer;

        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, -10.6f);
        }
        else if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, -10.6f);
        }

        if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, -10.6f);
        }
        else if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, -10.6f);
        }
    }
}