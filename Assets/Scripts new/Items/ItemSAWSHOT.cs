using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSAWSHOT : MonoBehaviour
{
    public int instances = 1;
    int timer = 0;
    public Vector3 bulletOffset = new Vector3(0, 0, 0);
    GameObject guyLatchedTo;
    public bool dogma = false;
    public bool canDoTheThing = false;
    public bool isAProc = false;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Bullet_Movement>() != null)
        {
            canDoTheThing = true;
            float procMoment = 100f - instances * 10 * gameObject.GetComponent<DealDamage>().procCoeff;
            float pringle = Random.Range(0f, 100f);
            bool isCrit = false;
            if (pringle > procMoment)
            {
                if (gameObject.GetComponent<ItemBOUNCY>() != null)
                {
                    Destroy(GetComponent<ItemBOUNCY>());
                }
                gameObject.AddComponent<ItemPIERCING>();
                gameObject.GetComponent<DealDamage>().onlyDamageOnce = false;
                isAProc = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dogma)
        {
            timer++;
        }
    }

    void Update()
    {
        if (dogma)
        {
            if (guyLatchedTo == null || timer == 100 * instances)
            {
                Destroy(gameObject);
            }

            transform.position = guyLatchedTo.transform.position + bulletOffset;
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), guyLatchedTo.GetComponent<Collider2D>(), false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isAProc)
        {
            dogma = true;
            guyLatchedTo = col.gameObject;
            bulletOffset = transform.position - guyLatchedTo.transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;
            gameObject.GetComponent<Collider2D>().offset = -2 * bulletOffset;
            gameObject.GetComponent<DealDamage>().finalDamageMult /= 5;
            gameObject.GetComponent<DealDamage>().tickInterval = 10;
        }
    }
}
