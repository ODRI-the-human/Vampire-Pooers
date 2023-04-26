using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawShotCreep : MonoBehaviour
{
    GameObject creepy;
    GameObject BOB;

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
        if (gameObject.GetComponent<DealDamage>().timer % 20 == 0)
        {
            GameObject Bongo = Instantiate(creepy, gameObject.GetComponent<SawRotation>().guyLatchedTo.transform.position, Quaternion.Euler(0,0,0));
            Bongo.transform.position = new Vector3(Bongo.transform.position.x, Bongo.transform.position.y, 0);
            Bongo.transform.localScale *= 0.35f;
            Bongo.GetComponent<SpriteRenderer>().color = Color.red;
            Bongo.GetComponent<DealDamage>().overwriteDamageCalc = true;
            Bongo.GetComponent<DealDamage>().damageAmt = 0.5f * gameObject.GetComponent<DealDamage>().damageAmt;
            Physics2D.IgnoreCollision(Bongo.GetComponent<Collider2D>(), BOB.GetComponent<Collider2D>(), true);
        }
    }
}
