using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSPLIT : MonoBehaviour
{
    Vector2 ShotVector;
    Rigidbody2D bulletRB;
    GameObject owner;
    float speed;
    public bool canSplit = true;
    public int instances = 1;
    GameObject Buuleter;

    void Start()
    {
        speed = gameObject.GetComponent<Bullet_Movement>().speed;

        if (gameObject.tag == "PlayerBullet")
        {
            Buuleter = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().playerBullet;
        }
        else
        {
            Buuleter = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().enemyBullet;
        }
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 bulletPos = new Vector2(col.transform.position.x, col.transform.position.y);

        if (canSplit && col.gameObject.tag != "Wall")
        {
            if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
            {
                ShotVector = speed * (bulletPos - enemyPos).normalized;
                GameObject Splitman1 = Instantiate(Buuleter, transform.position, transform.rotation);
                Splitman1.transform.localScale = 0.5f * gameObject.transform.localScale;
                Splitman1.GetComponent<DealDamage>().finalDamageMult *= 0.3f * gameObject.GetComponent<DealDamage>().finalDamageMult * instances;
                Splitman1.GetComponent<DealDamage>().knockBackCoeff = 0.5f * gameObject.GetComponent<DealDamage>().knockBackCoeff;
                owner = gameObject.GetComponent<DealDamage>().owner;
                Splitman1.GetComponent<DealDamage>().owner = owner;
                Splitman1.GetComponent<DealDamage>().damageBase += owner.GetComponent<Attack>().Crongus;
                Splitman1.AddComponent<ItemSPLIT>();
                Splitman1.GetComponent<ItemSPLIT>().canSplit = false;
                Splitman1.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
                bulletRB = Splitman1.GetComponent<Rigidbody2D>();
                bulletRB.velocity = new Vector3(ShotVector.x * Mathf.Cos(-Mathf.PI / 2) - ShotVector.y * Mathf.Sin(-Mathf.PI / 2), ShotVector.x * Mathf.Sin(-Mathf.PI / 2) + ShotVector.y * Mathf.Cos(-Mathf.PI / 2), 0);
                GameObject Splitman2 = Instantiate(Buuleter, transform.position, transform.rotation);
                Splitman2.transform.localScale = 0.5f * gameObject.transform.localScale;
                Splitman2.GetComponent<DealDamage>().finalDamageMult *= 0.3f * gameObject.GetComponent<DealDamage>().finalDamageMult * instances;
                Splitman2.GetComponent<DealDamage>().knockBackCoeff = 0.5f * gameObject.GetComponent<DealDamage>().knockBackCoeff;
                owner = gameObject.GetComponent<DealDamage>().owner;
                Splitman2.GetComponent<DealDamage>().owner = owner;
                Splitman2.GetComponent<DealDamage>().damageBase += owner.GetComponent<Attack>().Crongus;
                Splitman2.AddComponent<ItemSPLIT>();
                Splitman2.GetComponent<ItemSPLIT>().canSplit = false;
                Splitman2.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
                bulletRB = Splitman2.GetComponent<Rigidbody2D>();
                bulletRB.velocity = new Vector3(ShotVector.x * Mathf.Cos(Mathf.PI / 2) - ShotVector.y * Mathf.Sin(Mathf.PI / 2), ShotVector.x * Mathf.Sin(Mathf.PI / 2) + ShotVector.y * Mathf.Cos(Mathf.PI / 2), 0);
            }
        }
    }
}
