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

    bool dealTheDamage = true;

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
        dealTheDamage = false;
        RollOnHit(col.gameObject);
    }

    public void RollOnHit(GameObject gamer)
    {
        if (gamer.tag != "Wall")
        {
            if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
            {
                //RollOnHit();
                //GUY = gamer;
                Debug.Log("your mum");

                if (!owner.GetComponent<ItemELECTRIC>().enemiesEffected.Contains(gamer))
                {
                    owner.GetComponent<ItemELECTRIC>().enemiesEffected.Add(gamer);
                }

                gamer.GetComponent<Statuses>().hasElectric = 1;
                if (!gamer.GetComponent<Statuses>().iconOrder.Contains(2))
                {
                    gamer.GetComponent<Statuses>().iconOrder.Add(2);
                }
            }
        }

        masterObject = EntityReferencerGuy.Instance.master;

        if (dealTheDamage)
        {
            foreach (GameObject Gareth in owner.GetComponent<ItemELECTRIC>().enemiesEffected)
            {
                if (Gareth != null && Gareth != gamer)
                {
                    Gareth.GetComponent<HPDamageDie>().HP -= 10 * instances;
                    Gareth.GetComponent<HPDamageDie>().sprite.color = Color.red;
                    Gareth.GetComponent<HPDamageDie>().colorChangeTimer = 1;
                    masterObject.GetComponent<showDamageNumbers>().showDamage(Gareth.transform.position, 10 * instances, (int)DAMAGETYPES.ELECTRIC, false);
                }
            }
        }

        dealTheDamage = true;
    }

    void newWaveEffects()
    {
        enemiesEffected.Clear();
    }

    public void Undo()
    {
        Destroy(this);
    }
}
