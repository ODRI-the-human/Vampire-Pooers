using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleStatus : MonoBehaviour
{
    public Vector3 posertion = new Vector3(999, 999, 999);
    public LineRenderer line;

    public Vector3 hitboxPos;
    public float fuckAngle;
    public GameObject hitbox;

    public GameObject nearestFriend;
    public GameObject moleProj;

    bool doFunnyVisual;
    public bool giveFunnyVisual;
    int timer = 0;
    float electricRandAmt = 0.1f;

    public bool ignored;


    public float distanceFromNearest;
    public List<GameObject> mates = new List<GameObject>();
    public bool hasFired = false;
    Vector3 vectorMan;
    public Material warn;
    public Material shoot;
    public Material invis;
    public bool firstAndy = false;

    public GameObject controllingObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnLazer()
    {
        if (nearestFriend == null)
        {
            FindNearest(transform.position);

            line.SetPosition(0, transform.position);
            line.SetPosition(1, Vector3.Lerp(nearestFriend.transform.position, posertion, -0.05f));
            line.SetPosition(2, nearestFriend.transform.position);
            posertion = new Vector3(posertion.x, posertion.y, -0.1f);
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
            if (!nearestFriend.GetComponent<moleStatus>().firstAndy)
            {
                nearestFriend.GetComponent<moleStatus>().SpawnLazer();
            }
        }
    }

    void FindNearest(Vector3 posToUse)
    {
        mates.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        foreach (GameObject friend in enemies)
        {
            if (friend.GetComponent<moleStatus>() != null && friend != gameObject && !friend.GetComponent<moleStatus>().hasFired && (transform.position - friend.transform.position).magnitude < 50)
            {
                mates.Add(friend);
            }
        }

        if (mates.Count == 0)
        {
            foreach (GameObject friend in enemies)
            {
                if (friend.GetComponent<moleStatus>() != null && friend != gameObject && friend.GetComponent<moleStatus>().firstAndy && (transform.position - friend.transform.position).magnitude < 50)
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
        if (nearestFriend != null)
        {
            distanceFromNearest = (nearestFriend.transform.position - posToUse).magnitude;
        }
    }

    public void ActuallyShoot()
    {
        if (nearestFriend != null)// || firstAndy)
        {
            hitbox.GetComponent<enableHitbox>().enableHitboxer();
            giveFunnyVisual = true;
            line.material = shoot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = posertion;
    }

    void FixedUpdate()
    {
        timer++;

        if (nearestFriend == null && controllingObject != null)
        {
            if (controllingObject.GetComponent<moleGamingV3>().currentStep == 2)
            {
                controllingObject.GetComponent<moleGamingV3>().GetCancelled();
                controllingObject.GetComponent<moleGamingV3>().TargetOthers();
            }
            if (controllingObject.GetComponent<moleGamingV3>().currentStep == 3)
            {
                controllingObject.GetComponent<moleGamingV3>().GetCancelled();
            }
        }

        if (giveFunnyVisual && timer % 2 == 0)
        {
            line.SetPosition(1, new Vector3(Vector3.Lerp(nearestFriend.transform.position, posertion, 0.5f).x, Vector3.Lerp(nearestFriend.transform.position, posertion, 0.5f).y, 0) + new Vector3(Random.Range(-electricRandAmt, electricRandAmt), Random.Range(-electricRandAmt, electricRandAmt), -0.05f));
        }
    }
}
