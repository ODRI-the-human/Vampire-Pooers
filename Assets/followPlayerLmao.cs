using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerLmao : MonoBehaviour
{
    public GameObject Player;
    Vector3 mouseVector;
    Vector3 vectorMan;
    float fuckAngle;
    //public int whichGuy;

    //void Start()
    //{
    //    GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
    //    Player = gos[whichGuy];
    //}

    void Update()
    {
        if (Player == null)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -0.8f);// + new Vector3(0,0.1f,-1);

        vectorMan = Player.GetComponent<Attack>().vectorToTarget;
        Quaternion actualRotation = Quaternion.LookRotation(-vectorMan, new Vector3(0, 0, -1));

        transform.rotation = actualRotation;
        transform.Rotate(0, 0, 40, Space.World);
    }
}
