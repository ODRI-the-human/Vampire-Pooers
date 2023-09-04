using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawShotCreep : MonoBehaviour
{
    GameObject creepy;
    GameObject BOB;
    int timer = 0;

    void Start()
    {
        BOB = gameObject.GetComponent<SawRotation>().guyLatchedTo;

        if (gameObject.tag == "PlayerBullet")
        {
            creepy = EntityReferencerGuy.Instance.Creep;
        }
        else
        {
            creepy = EntityReferencerGuy.Instance.CreepHostile;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if (timer % 20 == 0)
        {
            GameObject Bongo = Instantiate(creepy, gameObject.GetComponent<SawRotation>().guyLatchedTo.transform.position, Quaternion.Euler(0,0,0));
            Bongo.transform.position = new Vector3(Bongo.transform.position.x, Bongo.transform.position.y, 0);
            Bongo.transform.localScale *= 0.35f;
            Bongo.GetComponent<SpriteRenderer>().color = Color.red;
            Bongo.GetComponent<DealDamage>().overwriteDamageCalc = true;
            Bongo.GetComponent<DealDamage>().finalDamageStat = 0.5f * gameObject.GetComponent<DealDamage>().finalDamageStat;
            Bongo.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
            Physics2D.IgnoreCollision(Bongo.GetComponent<Collider2D>(), BOB.GetComponent<Collider2D>(), true);
        }
    }
}
