using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleShit : MonoBehaviour
{

    public int timer = 0;
    GameObject player;
    public List<GameObject> mates = new List<GameObject>();
    public GameObject nearestFriend;
    public LineRenderer line;

    public bool goesFirst = false;
    public bool hasGone = false;
    public bool taken;

    Vector3 pos; //to prevent knockback from moving the enemies around.

    void Start()
    {
        player = GameObject.Find("newPlayer");
    }

    public void Bingus()
    {
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = pos;

        if (timer % 200 == 0)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), -2);
            while ((transform.position - player.transform.position).magnitude < 3)
            {
                transform.position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), -2);
            }
            transform.localScale = new Vector3(3,3,1);
            pos = transform.position;
            nearestFriend = null;
        }

        if ((timer + 130) % 200 == 0)
        {
            if (goesFirst)
            {
                Bingus();
            }
        }

        if ((timer + 129) % 200 == 0)
        {
            if (nearestFriend != null)
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, nearestFriend.transform.position);
            }
        }

        if ((timer + 35) % 200 == 0)
        {
            transform.localScale = new Vector3(0, 0, 0);
            transform.position = new Vector3(9999, 9999, 9999);
            mates.Clear();
            line.SetPosition(0, new Vector3(0,0,0));
            line.SetPosition(1, new Vector3(0, 0, 0));
            taken = false;
            hasGone = false;
            nearestFriend = null;
            pos = transform.position;
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
                    friend.GetComponent<moleShit>().line.SetPosition(0, new Vector3(0, 0, 0));
                    friend.GetComponent<moleShit>().line.SetPosition(1, new Vector3(0, 0, 0));
                }
            }
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        timer++;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (goesFirst)
        {
            GameObject Martin = mates[0];
            Martin.GetComponent<moleShit>().goesFirst = true;
            goesFirst = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (goesFirst)
        {
            GameObject Martin = mates[0];
            Martin.GetComponent<moleShit>().goesFirst = true;
            goesFirst = false;
        }
    }
}
