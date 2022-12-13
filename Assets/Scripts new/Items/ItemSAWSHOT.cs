using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSAWSHOT : MonoBehaviour
{
    public int instances = 1;
    int timer = 0;
    public Vector3 bulletOffset = new Vector3(0, 0, 0);
    public GameObject guyLatchedTo;
    public bool dogma = false;
    public bool canDoTheThing = false;
    public bool isAProc = false;

    GameObject Poop;

    public GameObject SawShotVisual;

    GameObject master;

    // Start is called before the first frame update
    void Start()
    {
        master = gameObject.GetComponent<DealDamage>().master;
        if (gameObject.GetComponent<Bullet_Movement>() != null)
        {
            canDoTheThing = true;
            float procMoment = 100f - 10 * gameObject.GetComponent<DealDamage>().procCoeff;
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
                gameObject.GetComponent<MeshFilter>().mesh = null;//master.GetComponent<EntityReferencerGuy>().saw;
                Poop = Instantiate(master.GetComponent<EntityReferencerGuy>().sawVisual, transform.position, transform.rotation);
                Poop.transform.SetParent(gameObject.transform);
                Poop.transform.localScale = transform.localScale;
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
                Destroy(Poop);
                Destroy(gameObject);
            }

            Poop.transform.position = guyLatchedTo.transform.position + bulletOffset;
            transform.position = guyLatchedTo.transform.position;
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), guyLatchedTo.GetComponent<Collider2D>(), false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isAProc)
        {
            dogma = true;
            guyLatchedTo = col.gameObject;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            bulletOffset = transform.position - guyLatchedTo.transform.position;
            Poop.transform.parent = null;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            gameObject.GetComponent<CircleCollider2D>().radius = 0.1f;
            gameObject.GetComponent<DealDamage>().finalDamageMult /= 5;
            gameObject.GetComponent<DealDamage>().tickInterval = 10;
            gameObject.AddComponent<SawShotCreep>();

            if (col.gameObject.tag == "Wall")
            {
                Destroy(Poop);
            }
        }
    }
}
