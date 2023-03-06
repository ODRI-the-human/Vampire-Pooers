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
        owner = gameObject.GetComponent<DealDamage>().owner;
        speed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        Buuleter = owner.GetComponent<Attack>().Bullet;
    }



    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 bulletPos = new Vector2(col.transform.position.x, col.transform.position.y);
        ShotVector = speed * (bulletPos - enemyPos).normalized;

        if (canSplit && col.gameObject.tag != "Wall" && gameObject.GetComponent<darkArtMovement>() != null)
        {
            if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject Splitman1 = Instantiate(Buuleter, transform.position, transform.rotation);
                    Splitman1.transform.localScale = 0.6f * instances * gameObject.transform.localScale;
                    Splitman1.GetComponent<DealDamage>().finalDamageMult *= 0.3f * gameObject.GetComponent<DealDamage>().finalDamageMult * instances;
                    Splitman1.GetComponent<DealDamage>().massCoeff = 0.5f * gameObject.GetComponent<DealDamage>().massCoeff;
                    Splitman1.GetComponent<DealDamage>().owner = owner;
                    Splitman1.GetComponent<DealDamage>().damageBase += owner.GetComponent<Attack>().Crongus;
                    Splitman1.GetComponent<Rigidbody2D>().simulated = true;
                    Splitman1.GetComponent<DealDamage>().isBulletClone = true;
                    Splitman1.GetComponent<Rigidbody2D>().velocity = new Vector3(ShotVector.x * Mathf.Cos(-((i * 2) - 1) * Mathf.PI / 2) - ShotVector.y * Mathf.Sin(-((i * 2) - 1) * Mathf.PI / 2), ShotVector.x * Mathf.Sin(-((i * 2) - 1) * Mathf.PI / 2) + ShotVector.y * Mathf.Cos(-((i * 2) - 1) * Mathf.PI / 2), 0);
                    Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), Splitman1.GetComponent<Collider2D>(), true);
                    Splitman1.GetComponent<ItemSPLIT>().canSplit = false;
                }
            }
        }
    }

    public void Undo()
    {
        //nothin
    }
}
