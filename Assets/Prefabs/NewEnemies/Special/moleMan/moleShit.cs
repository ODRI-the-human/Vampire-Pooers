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

    //public GameObject martin;

    Vector3 vectorMan;
    float fuckAngle;

    Vector3 pos; //to prevent knockback from moving the enemies around.
    Vector3 hitboxPos;

    void Start()
    {
        player = GameObject.Find("newPlayer");
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
            //(nearestFriend.transform.position - transform.position)
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = pos;

        if (hitbox != null)
        {
            hitbox.transform.position = hitboxPos;
        }

        if (timer % 200 == 0)
        {
            hasPositioned = false;
        }

        if (!hasPositioned)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), -2);
            while ((transform.position - player.transform.position).magnitude < 3)
            {
                transform.position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), -2);
            }
            transform.localScale = new Vector3(3, 3, 1);
            pos = transform.position;
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

        if ((timer + 130) % 200 == 0)
        {
            if (goesFirst)
            {
                Bingus();
            }
        }

        if ((timer + 35) % 200 == 0)
        {
            transform.localScale = new Vector3(0, 0, 0);
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

        if (nearestFriend == null && hasGone)
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

            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShit>() != null)
                {
                    if (friend.GetComponent<moleShit>().goesFirst)
                    {
                        friend.GetComponent<moleShit>().Bingus();
                    }
                }
            }
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        timer++;

        if (gameObject.GetComponent<HPDamageDie>().HP <= 0)
        {
            Destroy(hitbox);
        }
    }

    public void DestroyHitbox()
    {
        Destroy(hitbox);
    }
}
