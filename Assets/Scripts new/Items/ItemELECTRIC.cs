using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemELECTRIC : MonoBehaviour
{
    public List<GameObject> enemiesEffected = new List<GameObject>();
    public GameObject owner;
    public int instances = 1;

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            owner = gameObject;
        }
        else
        {
            owner = gameObject.GetComponent<DealDamage>().owner;
        }
    }

    void OnCollisionEnter2D(Collision2D col) // featuring 15 million if statements
    {
        if (col.gameObject.tag != "Wall")
        {
            if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
            {
                foreach (GameObject Gareth in owner.GetComponent<ItemELECTRIC>().enemiesEffected)
                {
                    if (Gareth != null)
                    {
                        Gareth.GetComponent<HPDamageDie>().HP -= 20;
                        Gareth.GetComponent<HPDamageDie>().sprite.color = Color.red;
                        Gareth.GetComponent<HPDamageDie>().colorChangeTimer = 1;
                    }
                }
                Debug.Log("your mum");

                if (!owner.GetComponent<ItemELECTRIC>().enemiesEffected.Contains(col.gameObject))
                {
                    owner.GetComponent<ItemELECTRIC>().enemiesEffected.Add(col.gameObject);
                }

                col.gameObject.GetComponent<Statuses>().hasElectric = 1;
                if (!col.gameObject.GetComponent<Statuses>().iconOrder.Contains(2))
                {
                    col.gameObject.GetComponent<Statuses>().iconOrder.Add(2);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            enemiesEffected.Clear();
        }
    }
}
