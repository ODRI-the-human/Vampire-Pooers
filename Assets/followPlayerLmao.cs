using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerLmao : MonoBehaviour
{
    public GameObject Player;
    Vector3 mouseVector;
    Vector3 vectorMan;
    Vector3 lastMoveDir;
    float fuckAngle;
    //public int whichGuy;

    void Start()
    {
        vectorMan = new Vector3(1, 0, 0);
        lastMoveDir = new Vector3(1, 0, 0);
    }

    void Update()
    {
        if (Player == null)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -0.8f);// + new Vector3(0,0.1f,-1);

        vectorMan = Player.GetComponent<Attack>().vectorToTarget;
        
        if (vectorMan == Vector3.zero)
        {
            if (Player.GetComponent<NewPlayerMovement>().moveDirection != Vector3.zero)
            {
                vectorMan = Player.GetComponent<NewPlayerMovement>().moveDirection;
            }
            else
            {
                vectorMan = lastMoveDir;
            }
        }

        Quaternion actualRotation = Quaternion.LookRotation(-vectorMan, new Vector3(0, 0, -1));

        transform.rotation = actualRotation;
        transform.Rotate(0, 0, 40, Space.World);

        lastMoveDir = vectorMan;
    }
}
