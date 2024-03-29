using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMARCEL : ItemScript
{
    GameObject marcelInstance;
    int timer = 2200;

    void Start()
    {
        marcelInstance = EntityReferencerGuy.Instance.marcelageloo;
        //timer = Random.Range(0, 1100);
    }

    void FixedUpdate()
    {
        if (timer > ((30 / instances) + 15) * 50)
        {
            GameObject marceller = Instantiate(marcelInstance, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.Euler(0, 0, 0));
            if (gameObject.tag == "Player")
            {
                marceller.tag = "PlayerBullet";
            }
            else
            {
                marceller.tag = "enemyBullet";
            }
            marceller.transform.localScale *= 1 + 0.5f * instances;
            marceller.GetComponent<marcelFunny>().owner = gameObject;
            timer = 0;
        }

        timer++;
    }
}
