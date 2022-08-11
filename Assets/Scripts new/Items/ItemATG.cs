using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITEMATG : MonoBehaviour
{
    public GameObject Player;
    public GameObject ATGMissile;
    public GameObject MasterObject;

    void Awake()
    {
        MasterObject = GameObject.Find("bigFuckingMasterObject");
        ATGMissile = MasterObject.GetComponent<EntityReferencerGuy>().ATGMissile;
        Player = GameObject.Find("newPlayer");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Instantiate(ATGMissile, Player.transform.position, Player.transform.rotation);
    }
}
