using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleShitV2 : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> mates = new List<GameObject>();
    public List<Vector3> badPositions = new List<Vector3>();
    public List<Vector3> rockPositions = new List<Vector3>();
    public GameObject nearestFriend;
    public GameObject moleProj;
    public LineRenderer line;

    public bool goesFirst = false;
    public bool hasFired = false;
    public bool taken;
    bool hasPositioned = false;
    public GameObject hitbox;

    public float distanceFromPlayer;
    public float distanceFromNearest;
    bool positionIsOkay;

    public Vector3 bumHead;
    Vector3 hitboxPos;
    float fuckAngle;
    Vector3 vectorMan;
    Vector3 pos;

    public GameObject camera;
    public GameObject master;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartCycle), 0.1f);
        hasPositioned = false;
        hasFired = false;
    }

    void Update()
    {
        if (hasPositioned)
        {
            transform.position = pos;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }

        if (nearestFriend == null && hasFired && !goesFirst) // if a mole gets killed.
        {
            line.SetPosition(0, new Vector3(0, 0, 0));
            line.SetPosition(1, new Vector3(0, 0, 0));

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShitV2>() != null)
                {
                    friend.GetComponent<moleShitV2>().DestroyHitbox();
                    friend.GetComponent<moleShitV2>().line.SetPosition(0, new Vector3(0, 0, 0));
                    friend.GetComponent<moleShitV2>().line.SetPosition(1, new Vector3(0, 0, 0));
                    friend.GetComponent<moleShitV2>().hasFired = false;
                    friend.GetComponent<moleShitV2>().hasPositioned = false;
                }
            }
        }

        if (gameObject.GetComponent<HPDamageDie>().HP < 50 && goesFirst)
        {
            nearestFriend.GetComponent<moleShitV2>().goesFirst = true;
            goesFirst = false;
        }
    }

    public void StartCycle()
    {
        mates.Clear();
        badPositions.Clear();
        rockPositions.Clear();

        if (goesFirst)
        {
            master = gameObject.GetComponent<DealDamage>().master;
            camera = master.GetComponent<EntityReferencerGuy>().camera;
            player = gameObject.GetComponent<Attack>().currentTarget;
            FindRockPositions();
            PickPosition();
        }
    }

    public void PickPosition()
    {
        bumHead = player.transform.position;
        positionIsOkay = false;
        while (!positionIsOkay)
        {
            bumHead = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 8) + camera.transform.position;
            bumHead.x = Mathf.Clamp(bumHead.x, -camera.GetComponent<cameraMovement>().xBound, camera.GetComponent<cameraMovement>().xBound);
            bumHead.y = Mathf.Clamp(bumHead.y, -camera.GetComponent<cameraMovement>().yBound, camera.GetComponent<cameraMovement>().yBound);
            bumHead.x = Mathf.Round(bumHead.x / 2) * 2;
            bumHead.y = Mathf.Round(bumHead.y / 2) * 2;
            bumHead.z = 0;
            CheckPositionAvailability(bumHead);
        }
        pos = new Vector3(bumHead.x, bumHead.y, 0);
        transform.position = pos;
        badPositions.Add(transform.position);
        distanceFromPlayer = (transform.position - player.transform.position).magnitude;
        Debug.Log("benenr");
        hasPositioned = true;
        PickRandomGuy();
        if (nearestFriend != null)
        {
            nearestFriend.GetComponent<moleShitV2>().camera = camera;
            nearestFriend.GetComponent<moleShitV2>().player = player;
            nearestFriend.GetComponent<moleShitV2>().master = master;
            nearestFriend.GetComponent<moleShitV2>().badPositions = badPositions;
            nearestFriend.GetComponent<moleShitV2>().rockPositions = rockPositions;
            nearestFriend.GetComponent<moleShitV2>().PickPosition();
        }

        if (goesFirst)
        {
            Invoke(nameof(spawnLazer), 1);
        }
    }

    void spawnLazer()
    {
        Invoke(nameof(Reset), 2);
        FindNearest(transform.position);

        line.SetPosition(0, transform.position + new Vector3(0, 0, -5));
        line.SetPosition(1, nearestFriend.transform.position + new Vector3(0, 0, -5));
        hitbox = Instantiate(moleProj, transform.position + new Vector3(9999, 9999, 9999) + 0.5f * (nearestFriend.transform.position - transform.position), transform.rotation);
        hitbox.GetComponent<CapsuleCollider2D>().size = new Vector2(0.2f, (nearestFriend.transform.position - transform.position).magnitude);
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
        hitbox.GetComponent<DealDamage>().owner = gameObject;
        hitbox.transform.position -= new Vector3(9999, 9999, 9999);
        hitboxPos = hitbox.transform.position;
        hasFired = true;
        nearestFriend.GetComponent<moleShitV2>().spawnLazer();
    }

    public void Reset()
    {
        transform.position = new Vector3(9999, 9999, 9999);
        mates.Clear();
        badPositions.Clear();
        line.SetPosition(0, new Vector3(0, 0, 0));
        line.SetPosition(1, new Vector3(0, 0, 0));
        hasFired = false;
        hasPositioned = false;
        nearestFriend = null;
        pos = transform.position;
        Destroy(hitbox);

        Invoke(nameof(StartCycle), 0.5f);
    }

    void CheckPositionAvailability(Vector3 posToUse)
    {
        bool bongus = true;

        if ((posToUse - player.transform.position).magnitude < 4)
        {
            bongus = false;
        }

        foreach (Vector3 place in badPositions)
        {
            if (place == posToUse)
            {
                bongus = false;
            }
        }

        foreach (Vector3 place in rockPositions)
        {
            if (place == posToUse)
            {
                bongus = false;
            }
        }

        positionIsOkay = bongus;
    }

    void PickRandomGuy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        nearestFriend = null;
        foreach (GameObject friend in enemies)
        {
            if (friend.GetComponent<moleShitV2>() != null && friend != gameObject)// && enemies.Length > 20)
            {
                if (!friend.GetComponent<moleShitV2>().hasPositioned && !friend.GetComponent<moleShitV2>().goesFirst)
                {
                    nearestFriend = friend;
                    break;
                }
            }
        }
    }

    void FindRockPositions()
    {
        rockPositions.Clear();
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject obstacle in obstacles)
        {
            rockPositions.Add(obstacle.transform.position);
        }
    }

    void FindNearest(Vector3 posToUse)
    {
        mates.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        foreach (GameObject friend in enemies)
        {
            if (friend.GetComponent<moleShitV2>() != null && friend != gameObject && !friend.GetComponent<moleShitV2>().hasFired)
            {
                mates.Add(friend);
            }
        }

        if (mates.Count == 0)
        {
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShitV2>() != null && friend != gameObject && friend.GetComponent<moleShitV2>().goesFirst)
                {
                    mates.Add(friend);
                }
            }
        }

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = posToUse;

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

        nearestFriend = closest;
        distanceFromNearest = (nearestFriend.transform.position - posToUse).magnitude;
    }

    public void DestroyHitbox()
    {
        Destroy(hitbox);
    }
}
