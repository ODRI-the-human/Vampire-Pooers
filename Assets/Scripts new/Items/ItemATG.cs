using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemATG : MonoBehaviour
{
    public GameObject ATGMissile;
    public GameObject MasterObject;
    GameObject owner;
    bool hostile;
    float procMoment;
    public int instances = 1;
    float pringle;

    void Start()
    {
        MasterObject = GameObject.Find("bigFuckingMasterObject");

        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "Player")
        {
            hostile = false;
            ATGMissile = MasterObject.GetComponent<EntityReferencerGuy>().ATGMissile;
        }
        else
        {
            hostile = true;
            ATGMissile = MasterObject.GetComponent<EntityReferencerGuy>().ATGMissileHostile;
        }

        owner = gameObject.GetComponent<DealDamage>().owner;
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

                if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
                {
                    GameObject ARSEMAN = Instantiate(ATGMissile, owner.transform.position, owner.transform.rotation);
                    ARSEMAN.GetComponent<MissileTracking>().owner = owner;

                    if (hostile)
                    {
                        ARSEMAN.tag = "enemyBullet";
                    }
                    else
                    {
                        ARSEMAN.tag = "PlayerBullet";
                    }
                }
            }
        }
    }
}
