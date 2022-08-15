using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSPLIT : MonoBehaviour
{
    Vector2 ShotVector;
    Rigidbody2D bulletRB;
    float speed;
    public bool canSplit = false;
    public int instances;

    void Start()
    {
        if (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet")
        {
            canSplit = true;
        }
        speed = gameObject.GetComponent<Bullet_Movement>().speed;

        instances = 1;
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 bulletPos = new Vector2(col.transform.position.x, col.transform.position.y);

        if (col.gameObject.tag != "Wall" && canSplit)
        {
            ShotVector = speed * (bulletPos - enemyPos).normalized;
            GameObject Splitman1 = Instantiate(gameObject, transform.position, transform.rotation);
            Splitman1.transform.localScale = 0.5f * gameObject.transform.localScale;
            Splitman1.GetComponent<DealDamage>().damageMult = 0.3f * gameObject.GetComponent<DealDamage>().damageMult * instances;
            Splitman1.GetComponent<ItemSPLIT>().canSplit = false;
            bulletRB = Splitman1.GetComponent<Rigidbody2D>();
            bulletRB.velocity = new Vector3(ShotVector.x * Mathf.Cos(-Mathf.PI / 2) - ShotVector.y * Mathf.Sin(-Mathf.PI / 2), ShotVector.x * Mathf.Sin(-Mathf.PI / 2) + ShotVector.y * Mathf.Cos(-Mathf.PI / 2), 0);
            GameObject Splitman2 = Instantiate(gameObject, transform.position, transform.rotation);
            Splitman2.transform.localScale = 0.5f * gameObject.transform.localScale;
            Splitman2.GetComponent<DealDamage>().damageMult = 0.3f * gameObject.GetComponent<DealDamage>().damageMult * instances;
            Splitman2.GetComponent<ItemSPLIT>().canSplit = false;
            bulletRB = Splitman2.GetComponent<Rigidbody2D>();
            bulletRB.velocity = new Vector3(ShotVector.x * Mathf.Cos(Mathf.PI / 2) - ShotVector.y * Mathf.Sin(Mathf.PI / 2), ShotVector.x * Mathf.Sin(Mathf.PI / 2) + ShotVector.y * Mathf.Cos(Mathf.PI / 2), 0);
        }
    }
}
