using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleShit : MonoBehaviour
{

    public int timer = 0;
    GameObject player;
    public List<GameObject> mates = new List<GameObject>();
    public GameObject nearestFriend;
    public GameObject moleProj;
    public LineRenderer line;

    public bool goesFirst = false;
    public bool hasGone = false;
    public bool taken;
    bool hasPositioned = false;
    public GameObject hitbox;

    Vector3 bumHead;

    //public GameObject martin;

    Vector3 vectorMan;
    float fuckAngle;

    GameObject camera;

    Vector3 pos = new Vector3(9999,9999,9999); //to prevent knockback from moving the enemies around.
    Vector3 hitboxPos;

    Color normie;
    Color repos;

    void Awake()
    {
        player = GameObject.Find("newPlayer");
        camera = GameObject.Find("Main Camera");
        normie = gameObject.GetComponent<SpriteRenderer>().color;
        repos = gameObject.GetComponent<SpriteRenderer>().color;
        repos.a = 0;
    }

    public void Bingus()
    {
        mates.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        foreach (GameObject friend in enemies)
        {
            if (friend.GetComponent<moleShit>() != null && friend != gameObject)
            {
                if (!friend.GetComponent<moleShit>().taken && !friend.GetComponent<moleShit>().hasGone)
                {
                    mates.Add(friend);
                }
            }
        }

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        nearestFriend = null;

        foreach (GameObject mate in mates)
        {
            Vector3 diff = mate.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = mate;
                distance = curDistance;
            }
        }

        if (mates.Count == 0)
        {
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShit>() != null)
                {
                    if (friend.GetComponent<moleShit>().goesFirst)
                    {
                        closest = friend;
                    }
                }
            }
        }

        hasGone = true;
        nearestFriend = closest;
        nearestFriend.GetComponent<moleShit>().taken = true;
        if (!nearestFriend.GetComponent<moleShit>().hasGone)
        {
            nearestFriend.GetComponent<moleShit>().Bingus();
        }

        if (nearestFriend != null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, nearestFriend.transform.position);
            hitbox = Instantiate(moleProj, transform.position + 0.5f * (nearestFriend.transform.position - transform.position), transform.rotation);
            hitbox.GetComponent<CapsuleCollider2D>().size = new Vector2(0.2f, (nearestFriend.transform.position - transform.position).magnitude);
            hitbox.GetComponent<CapsuleCollider2D>().enabled = false;
            hitbox.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            hitbox.GetComponent<DealDamage>().owner = gameObject;
            hitbox.GetComponent<DealDamage>().finalDamageMult *= gameObject.GetComponent<DealDamage>().finalDamageMult;

            vectorMan = nearestFriend.transform.position - transform.position;

            if (vectorMan.y > 0 && vectorMan.x > 0)
            {
                fuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
            }
            if (vectorMan.y > 0 && vectorMan.x < 0)
            {
                fuckAngle = 180 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
            }
            if (vectorMan.y < 0 && vectorMan.x < 0)
            {
                fuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 180;
            }
            if (vectorMan.y < 0 && vectorMan.x > 0)
            {
                fuckAngle = 90 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 270;
            }
            hitbox.transform.Rotate(0, 0, fuckAngle + 90, Space.World);
            hitboxPos = hitbox.transform.position;
            hitbox.GetComponent<DealDamage>().owner = gameObject;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        transform.position = pos - new Vector3(0, 0, 1);

        if (hitbox != null)
        {
            hitbox.transform.position = hitboxPos;
        }

        if (timer % 200 == 0)
        {
            hasPositioned = false;
            gameObject.GetComponent<SpriteRenderer>().color = repos;
        }

        if (!hasPositioned)
        {
            randomisePos();
            nearestFriend = null;

            mates.Clear();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShit>() != null && friend != gameObject)
                {
                    if (!friend.GetComponent<moleShit>().taken && !friend.GetComponent<moleShit>().hasGone)
                    {
                        mates.Add(friend);
                    }
                }
            }

            hasPositioned = true;
        }

        if ((timer + 190) % 200 == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = normie;
        }

        if ((timer + 165) % 200 == 0)
        {
            taken = false;
        }

        if ((timer + 130) % 200 == 0)
        {
            if (goesFirst)
            {
                Bingus();
            }
        }

        if ((timer + 35) % 200 == 0)
        {
            transform.position = new Vector3(9999, 9999, 9999);
            mates.Clear();
            line.SetPosition(0, new Vector3(0, 0, 0));
            line.SetPosition(1, new Vector3(0, 0, 0));
            taken = false;
            hasGone = false;
            nearestFriend = null;
            pos = transform.position;
            Destroy(hitbox);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShit>() != null && friend != gameObject)
                {
                    if (!friend.GetComponent<moleShit>().taken && !friend.GetComponent<moleShit>().hasGone)
                    {
                        mates.Add(friend);
                    }
                }
            }
        }

        if (nearestFriend == null && hasGone) // if a mole gets killed.
        {
            line.SetPosition(0, new Vector3(0, 0, 0));
            line.SetPosition(1, new Vector3(0, 0, 0));

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShit>() != null)
                {
                    friend.GetComponent<moleShit>().DestroyHitbox();
                    friend.GetComponent<moleShit>().line.SetPosition(0, new Vector3(0, 0, 0));
                    friend.GetComponent<moleShit>().line.SetPosition(1, new Vector3(0, 0, 0));
                    friend.GetComponent<moleShit>().hasGone = false;
                    friend.GetComponent<moleShit>().taken = false;
                }
            }

            //foreach (GameObject friend in enemies) // makes the moles retarget.
            //{
            //    if (friend.GetComponent<moleShit>() != null)
            //    {
            //        if (friend.GetComponent<moleShit>().goesFirst)
            //        {
            //            friend.GetComponent<moleShit>().Bingus();
            //        }
            //    }
            //}
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        timer++;

        if (gameObject.GetComponent<HPDamageDie>().HP <= 0)
        {
            Destroy(hitbox);
        }
    }

    void randomisePos()
    {
        bumHead = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 8) + camera.transform.position;
        bumHead.x = Mathf.Clamp(bumHead.x, -camera.GetComponent<cameraMovement>().xBound, camera.GetComponent<cameraMovement>().xBound);
        bumHead.y = Mathf.Clamp(bumHead.y, -camera.GetComponent<cameraMovement>().yBound, camera.GetComponent<cameraMovement>().yBound);
        bumHead.x = Mathf.Round(bumHead.x / 2) * 2;
        bumHead.y = Mathf.Round(bumHead.y / 2) * 2;
        while ((bumHead - player.transform.position).magnitude < 5)
        {
            bumHead = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 8) + camera.transform.position;
            bumHead.x = Mathf.Clamp(bumHead.x, -camera.GetComponent<cameraMovement>().xBound, camera.GetComponent<cameraMovement>().xBound);
            bumHead.y = Mathf.Clamp(bumHead.y, -camera.GetComponent<cameraMovement>().yBound, camera.GetComponent<cameraMovement>().yBound);
            bumHead.x = Mathf.Round(bumHead.x / 2) * 2;
            bumHead.y = Mathf.Round(bumHead.y / 2) * 2;
        }
        bumHead = new Vector3(bumHead.x, bumHead.y, 0);
        pos = bumHead;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Wall" || col.gameObject.tag == "Hostile") && timer > 0)
        {
            randomisePos();
        }
    }

    public void DestroyHitbox()
    {
        Destroy(hitbox);
    }
}
