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

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        owner = gameObject.GetComponent<DealDamage>().owner;
        Buuleter = owner.GetComponent<Attack>().Bullet;
        if (gameObject.GetComponent<Rigidbody2D>() != null)
        {
            speed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
        else
        {
            speed = 15;
        }
    }

    // Start is called before the first frame update
    void RollOnHit(GameObject[] gameObjects)
    {
        GameObject victim = gameObjects[0];

        if (gameObject.GetComponent<meleeGeneral>() == null)
        {
            Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 bulletPos = new Vector2(victim.transform.position.x, victim.transform.position.y);
            ShotVector = speed * (bulletPos - enemyPos).normalized;

            Debug.Log("Splimt");
            if (canSplit && victim.tag != "Wall")
            {
                if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
                {
                    for (int i = 0; i < 2; i++)
                    {
                        GameObject Splitman1 = Instantiate(Buuleter, transform.position, transform.rotation);
                        Splitman1.transform.localScale = 0.8f * instances * gameObject.transform.localScale;
                        Splitman1.GetComponent<DealDamage>().finalDamageMult *= 0.3f * gameObject.GetComponent<DealDamage>().finalDamageMult * instances;
                        Splitman1.GetComponent<DealDamage>().massCoeff = 0.5f * gameObject.GetComponent<DealDamage>().massCoeff;
                        Splitman1.GetComponent<DealDamage>().owner = owner;
                        Splitman1.GetComponent<DealDamage>().damageBase += owner.GetComponent<Attack>().Crongus;
                        Splitman1.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
                        Splitman1.GetComponent<Rigidbody2D>().simulated = true;
                        Splitman1.GetComponent<Bullet_Movement>().isPooledBullet = false;
                        Physics2D.IgnoreCollision(victim.GetComponent<Collider2D>(), Splitman1.GetComponent<Collider2D>(), true);
                        //Splitman1.AddComponent<ItemSPLIT>();
                        //Splitman1.GetComponent<ItemSPLIT>().canSplit = false;
                        speed = 15;
                        ShotVector = speed * (owner.transform.position - victim.transform.position).normalized;
                        Splitman1.transform.position = victim.transform.position;
                        Splitman1.transform.localScale = 0.3f * new Vector3(1, 1, 1);
                        Splitman1.GetComponent<Rigidbody2D>().velocity = new Vector3(ShotVector.x * Mathf.Cos(-((i * 2) - 1) * Mathf.PI / 2) - ShotVector.y * Mathf.Sin(-((i * 2) - 1) * Mathf.PI / 2), ShotVector.x * Mathf.Sin(-((i * 2) - 1) * Mathf.PI / 2) + ShotVector.y * Mathf.Cos(-((i * 2) - 1) * Mathf.PI / 2), 0);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject owner = gameObject.GetComponent<DealDamage>().owner;
                StartCoroutine(owner.GetComponent<Attack>().SpawnMelee(Mathf.PI * (1 + 2 * i) / 2, victim.transform.position - owner.transform.position, victim, 0.3f * instances * gameObject.GetComponent<DealDamage>().finalDamageMult, 0, 0.8f));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canSplit)
        {
            Destroy(gameObject);
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
