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
    public bool giveFunnyVisual = false;
    bool hasPositioned = false;
    public GameObject hitbox;

    public GameObject zapAudio;
    GameObject spawnedZap;

    public float distanceFromPlayer;
    public float distanceFromNearest;
    bool positionIsOkay;

    public Vector3 bumHead;
    Vector3 hitboxPos;
    float fuckAngle;
    Vector3 vectorMan;
    public Vector3 pos;

    public GameObject camera;
    public GameObject master;

    public Material warn;
    public Material shoot;
    public Material invis;

    float electricRandAmt = 0.1f;
    int timer = 0;

    bool goesFirstExists = true;

    public float speedDebuffAmt;

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

        if (nearestFriend == null && hasFired) // if a mole gets killed.
        {
            line.SetPosition(0, new Vector3(0, 0, 0));
            line.SetPosition(1, new Vector3(0, 0, 0));
            line.SetPosition(2, new Vector3(0, 0, 0));
            giveFunnyVisual = false;
            if (goesFirst)
            {
                Destroy(spawnedZap);
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShitV2>() != null)
                {
                    friend.GetComponent<moleShitV2>().DestroyHitbox();
                    friend.GetComponent<moleShitV2>().line.SetPosition(0, new Vector3(0, 0, -0.05f));
                    friend.GetComponent<moleShitV2>().line.SetPosition(1, new Vector3(0, 0, -0.05f));
                    friend.GetComponent<moleShitV2>().line.SetPosition(2, new Vector3(0, 0, -0.05f));
                    friend.GetComponent<moleShitV2>().hasFired = false;
                    friend.GetComponent<moleShitV2>().hasPositioned = false;
                    friend.GetComponent<moleShitV2>().giveFunnyVisual = false;
                    if (friend.GetComponent<moleShitV2>().giveFunnyVisual == goesFirst)
                    {
                        Destroy(friend.GetComponent<moleShitV2>().spawnedZap);
                    }
                }
            }
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        timer++;

        if (giveFunnyVisual && timer % 2 == 0)
        {
            line.SetPosition(1, new Vector3(Vector3.Lerp(nearestFriend.transform.position, pos, 0.5f).x, Vector3.Lerp(nearestFriend.transform.position, pos, 0.5f).y, 0) + new Vector3(Random.Range(-electricRandAmt, electricRandAmt), Random.Range(-electricRandAmt, electricRandAmt), -0.05f));
        }

        // if there's 3 seconds of downtime, for whatever reason (this sometimes happens when a mole *is* tagged to go first, but doesn't for some reason)
        if (timer >= Mathf.Round(150 / speedDebuffAmt))
        {
            StartCycle();
        }

        if (timer == 5)
        {
            speedDebuffAmt = gameObject.GetComponent<Attack>().stopwatchDebuffAmount;
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
        timer = 0;

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
        pos = new Vector3(bumHead.x, bumHead.y, -0.1f);
        transform.position = pos;
        badPositions.Add(transform.position);
        distanceFromPlayer = (transform.position - player.transform.position).magnitude;
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
            Invoke(nameof(spawnLazer), (0.5f / speedDebuffAmt));
        }
    }

    void spawnLazer()
    {
        timer = 0;

        Invoke(nameof(Reset), (2.5f / speedDebuffAmt));
        Invoke(nameof(enableHitbox), (0.5f / speedDebuffAmt));
        FindNearest(transform.position);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, Vector3.Lerp(nearestFriend.transform.position, pos, -0.05f));
        line.SetPosition(2, nearestFriend.transform.position);
        pos = new Vector3(pos.x, pos.y, -0.1f);
        line.material = warn;
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

        if (vectorMan.x == 0 && vectorMan.y != 0)
        {
            fuckAngle = 90;
        }

        if (vectorMan.x != 0 && vectorMan.y == 0)
        {
            fuckAngle = 0;
        }

        hitbox.transform.Rotate(0, 0, fuckAngle + 90, Space.World);
        hitbox.GetComponent<DealDamage>().owner = gameObject;
        hitbox.transform.position -= new Vector3(9999, 9999, 9999);
        hitboxPos = hitbox.transform.position;
        hasFired = true;
        if (!nearestFriend.GetComponent<moleShitV2>().goesFirst)
        {
            nearestFriend.GetComponent<moleShitV2>().spawnLazer();
        }
    }

    void enableHitbox()
    {
        timer = 0;

        hitbox.GetComponent<enableHitbox>().enableHitboxer();
        giveFunnyVisual = true;
        line.material = shoot;

        if (goesFirst)
        {
            spawnedZap = Instantiate(zapAudio);
            spawnedZap.GetComponent<ownerDestroy>().owner = gameObject;
        }
    }

    public void Reset()
    {
        timer = 0;

        transform.position = new Vector3(9999, 9999, 9999);
        mates.Clear();
        badPositions.Clear();
        line.SetPosition(0, new Vector3(0, 0, 0));
        line.SetPosition(1, new Vector3(0, 0, 0));
        line.SetPosition(2, new Vector3(0, 0, 0));
        hasFired = false;
        hasPositioned = false;
        nearestFriend = null;
        giveFunnyVisual = false;
        pos = transform.position;
        Destroy(hitbox);
        line.material = invis;
        Destroy(spawnedZap);

        Invoke(nameof(StartCycle), (0.5f / speedDebuffAmt));
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
            if (friend.GetComponent<moleShitV2>() != null && friend != gameObject && !friend.GetComponent<moleShitV2>().hasFired && (transform.position - friend.transform.position).magnitude < 50)
            {
                mates.Add(friend);
            }
        }

        if (mates.Count == 0)
        {
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleShitV2>() != null && friend != gameObject && friend.GetComponent<moleShitV2>().goesFirst && (transform.position - friend.transform.position).magnitude < 30)
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

    public void ApplyOwnOnDeaths()
    {
        if (goesFirst)
        {
            if (nearestFriend == null)
            {
                PickRandomGuy();
            }

            nearestFriend.GetComponent<moleShitV2>().goesFirst = true;
        }
        if (nearestFriend == null)
        {
            PickRandomGuy();
        }
    }
}
