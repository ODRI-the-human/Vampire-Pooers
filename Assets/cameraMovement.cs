using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public GameObject[] players;
    public float amountToChangeWithMouse = 0.05f;
    public float moveSpeed = 0.04f;
    public float camShakeAmount = 1.5f;
    public bool moveCamera = true;
    int shakeTimer;

    //public GameObject LeftBorder;
    //public GameObject RightBorder;
    //public GameObject TopBorder;
    //public GameObject BottomBorder;

    public float xBound;
    public float yBound;

    void Start()
    {
        //xBound = RightBorder.transform.position.x - 10;
        //yBound = TopBorder.transform.position.y - 6;
        xBound = transform.parent.gameObject.GetComponent<GenerateTerrain>().mapHeight - 10.7f;
        yBound = transform.parent.gameObject.GetComponent<GenerateTerrain>().mapHeight - 6;
        //CheckAlivePlayers();
    }

    //public void CheckAlivePlayers()
    //{
    //    Debug.Log("cocke");
    //}

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
        {
            Vector3 avgVector = Vector3.zero;

            foreach (GameObject player in players)
            {
                if (player != null)
                {
                    avgVector += player.transform.position + amountToChangeWithMouse * new Vector3(player.GetComponent<Attack>().vectorToTarget.x, player.GetComponent<Attack>().vectorToTarget.y, 0);
                }
            }

            avgVector /= players.Length;

            Vector3 distFromPlayer = avgVector - transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10.6f) + moveSpeed * distFromPlayer;

            //Vector3 distFromPlayer = Player.transform.position - transform.position + amountToChangeWithMouse * new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y, 0).normalized;
            //distFromPlayer.z = 0;
            //transform.position = new Vector3(transform.position.x, transform.position.y, -10.6f) + moveSpeed * distFromPlayer;

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

            if (shakeTimer > 0)
            {
                float xRand = Random.Range(-0.5f, 0.5f);
                float yRand = Random.Range(-0.5f, 0.5f);
                transform.position = transform.position + camShakeAmount * 0.01f * shakeTimer * new Vector3(xRand, yRand, 0);
                shakeTimer--;
            }
        }
    }

    public void CameraShake(int amount)
    {
        if (amount > shakeTimer)
        {
            shakeTimer = amount;
        }
    }
}
