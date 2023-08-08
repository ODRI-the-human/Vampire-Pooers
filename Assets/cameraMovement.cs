using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public GameObject[] players;
    float amountToChangeWithMouse = 1.4f;
    float moveSpeed = 0.08f;
    public Vector3 camPushVec = Vector3.zero; // This is for things that do camera shake by 'pushing' the camera in a direction
    float timeOfLastPush = 0f;
    public float shakeAmount = 0;
    public bool moveCamera = true;

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

            //Vector3 sinShake = Mathf.Sin(Time.time * shakeAmount) * camPushVec;
            float recipPush = Mathf.Pow(Time.time - timeOfLastPush + 1, -80.1f); // Increase the magnitude of the power to make the push decay faster.
            transform.position = transform.position + camPushVec * recipPush / 200f;
        }
    }

    //void FixedUpdate()
    //{
    //    //camPushVec *= 0.15f;
    //}

    public void CameraShake(float amount, Vector3 pushDir)
    {
        if (pushDir == Vector3.zero)
        {
            camPushVec = amount * new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, 0).normalized;
        }
        else
        {
            camPushVec = pushDir;
        }
        timeOfLastPush = Time.time;
        //if (amount > shakeTimer)
        //{
        //    shakeTimer = amount;
        //}
    }
}
