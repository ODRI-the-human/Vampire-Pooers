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

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
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
        //dealTheDamage = false;
        //RollOnHit(col.gameObject);
    }

    void RollOnHit(GameObject[] objects)
    {
        GameObject gamer = objects[0];

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

                gamer.GetComponent<Statuses>().AddStatus((int)STATUSES.ELECTRIC, 0, gameObject);
            }
        }

        masterObject = EntityReferencerGuy.Instance.master;

        if (gameObject.GetComponent<Attack>() != null) // If the user is the player
        {
            foreach (GameObject Gareth in owner.GetComponent<ItemELECTRIC>().enemiesEffected)
            {
                if (Gareth != null && Gareth != gamer)
                {
                    //Gareth.GetComponent<HPDamageDie>().HP -= 10 * instances;
                    //Gareth.GetComponent<HPDamageDie>().sprite.color = Color.red;
                    Gareth.GetComponent<HPDamageDie>().Hurty(10 * instances, false, 0, (int)DAMAGETYPES.ELECTRIC, true, null, false);
                    //Gareth.GetComponent<HPDamageDie>().colorChangeTimer = 1;
                    //masterObject.GetComponent<showDamageNumbers>().showDamage(Gareth.transform.position, 10 * instances, (int)DAMAGETYPES.ELECTRIC, false);
                }
            }
        }
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