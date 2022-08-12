using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemATG : MonoBehaviour
{
    public GameObject Player;
    public GameObject ATGMissile;
    public GameObject MasterObject;
    float procMoment;
    public int instances = 1;
    float pringle;

    void Start()
    {
        MasterObject = GameObject.Find("bigFuckingMasterObject");
        ATGMissile = MasterObject.GetComponent<EntityReferencerGuy>().ATGMissile;
        Player = GameObject.Find("newPlayer");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Wall")
        {
            procMoment = 100f - instances * 10 * gameObject.GetComponent<DealDamage>().procCoeff;
            pringle = Random.Range(0f, 100f);
            Debug.Log("Poo: " + pringle.ToString());
            Debug.Log("Ass: " + procMoment.ToString());
            if (pringle > procMoment)
            {
                Instantiate(ATGMissile, Player.transform.position, Player.transform.rotation);
            }
        }
    }
}
