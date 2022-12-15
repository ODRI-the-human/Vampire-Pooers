using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemELECTRIC : MonoBehaviour
{
    public GameObject masterObject;
    public List<GameObject> enemiesEffected = new List<GameObject>();
    public GameObject owner;
    public int instances = 1;

    GameObject GUY;

    void Start()
    {
        if (!gameObject.GetComponent<DealDamage>().isBulletClone)
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
    }

    void OnCollisionEnter2D(Collision2D col) // featuring 15 million if statements
    {
        if (col.gameObject.tag != "Wall")
        {
            if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
            {
                //RollOnHit();
                //GUY = col.gameObject;
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

    public void RollOnHit(GameObject loser)
    {
        masterObject = gameObject.GetComponent<DealDamage>().master;

        foreach (GameObject Gareth in owner.GetComponent<ItemELECTRIC>().enemiesEffected)
        {
            if (Gareth != null && Gareth != loser)
            {
                Gareth.GetComponent<HPDamageDie>().HP -= 10 * instances;
                Gareth.GetComponent<HPDamageDie>().sprite.color = Color.red;
                Gareth.GetComponent<HPDamageDie>().colorChangeTimer = 1;
                masterObject.GetComponent<showDamageNumbers>().showDamage(Gareth.transform.position, 10 * instances, (int)DAMAGETYPES.ELECTRIC, false);
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

    public void Undo()
    {
        //nothin
    }
}
