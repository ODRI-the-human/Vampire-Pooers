using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleGamingV3 : MonoBehaviour
{
    public List<GameObject> moles = new List<GameObject>();
    public List<GameObject> unignoredMoles = new List<GameObject>();
    public List<Vector3> rockPositions = new List<Vector3>();
    public List<Vector3> badPositions = new List<Vector3>();

    public GameObject camera;

    bool positionIsOkay;

    GameObject player;
    Vector3 bumHead;

    public GameObject zapAudio;
    public GameObject spawnedZap;

    public bool doCycle = false;

    float stopwatchDebuffAmount = 1;

    public int currentStep = 0; //keeps track of what step of the cycle it's up to.

    void Start()
    {
        player = gameObject.GetComponent<EntityReferencerGuy>().playerInstance;
        camera = gameObject.GetComponent<EntityReferencerGuy>().camera;
    }

    public void CheckForStopWatch()
    {
        if (player.GetComponent<ItemSTOPWATCH>() != null)
        {
            stopwatchDebuffAmount = 1 / (0.4f * player.GetComponent<ItemSTOPWATCH>().instances + 1);
        }
    }

    public void StartCycle()
    {
        Invoke(nameof(SetPositions), 0.5f / stopwatchDebuffAmount);
    }

    void SetPositions()
    {
        badPositions.Clear();
        rockPositions.Clear();

        GetAllMoles();
        foreach (GameObject mole in moles)
        {
            bumHead = player.transform.position;
            positionIsOkay = false;
            FindRockPositions();
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
            mole.GetComponent<moleStatus>().posertion = new Vector3(bumHead.x, bumHead.y, -0.1f);
            badPositions.Add(bumHead);

            currentStep = 1;
        }

        if (doCycle)
        {
            Invoke(nameof(TargetOthers), 0.5f / stopwatchDebuffAmount);
            Invoke(nameof(Shooty), 1 / stopwatchDebuffAmount);//1);
        }
    }
    
    public void TargetOthers()
    {
        GetAllMoles();
        moles[0].GetComponent<moleStatus>().firstAndy = true;
        moles[0].GetComponent<moleStatus>().SpawnLazer();


        currentStep = 2;
        //foreach (GameObject mole in moles)
        //{
        //    GetUnignoredMoles();
        //    FindNearest(mole.transform.position);
        //    mole.GetComponent<moleStatus>().nearestFriend = nearestFriend;

        //    //mole.GetComponent<moleStatus>().line.SetPosition(0, transform.position);
        //    //mole.GetComponent<moleStatus>().line.SetPosition(1, Vector3.Lerp(nearestFriend.transform.position, mole.GetComponent<moleStatus>().posertion, -0.05f));
        //    //mole.GetComponent<moleStatus>().line.SetPosition(2, nearestFriend.transform.position);
        //    mole.GetComponent<moleStatus>().line.material = warn;
        //    //mole.GetComponent<moleStatus>().createHitbox();
        //    mole.GetComponent<moleStatus>().ignored = true;
        //}
    }

    void Shooty()
    {
        GetAllMoles();

        if (moles.Count > 0)
        {
            spawnedZap = Instantiate(zapAudio);
        }
        
        foreach (GameObject mole in moles)
        {
            mole.GetComponent<moleStatus>().ActuallyShoot();
        }

        if (doCycle)
        {
            Invoke(nameof(Reset), 1.5f / stopwatchDebuffAmount);
        }

        currentStep = 3;
    }

    void Reset()
    {
        GetAllMoles();
        foreach (GameObject mole in moles)
        {
            mole.GetComponent<moleStatus>().mates.Clear();
            mole.GetComponent<moleStatus>().line.SetPosition(0, new Vector3(0, 0, 0));
            mole.GetComponent<moleStatus>().line.SetPosition(1, new Vector3(0, 0, 0));
            mole.GetComponent<moleStatus>().line.SetPosition(2, new Vector3(0, 0, 0));
            mole.GetComponent<moleStatus>().hasFired = false;
            mole.GetComponent<moleStatus>().nearestFriend = null;
            mole.GetComponent<moleStatus>().giveFunnyVisual = false;
            Destroy(mole.GetComponent<moleStatus>().hitbox);
            mole.GetComponent<moleStatus>().line.material = mole.GetComponent<moleStatus>().invis;
            mole.GetComponent<moleStatus>().posertion = new Vector3(9999, 9999, 9999);
        }
        Destroy(spawnedZap);

        badPositions.Clear();
        rockPositions.Clear();

        if (doCycle)
        {
            Invoke(nameof(SetPositions), 0.5f / stopwatchDebuffAmount);
        }

        currentStep = 4;
    }

    void GetAllMoles()
    {
        moles.Clear();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        foreach (GameObject friend in enemies)
        {
            if (friend.GetComponent<moleStatus>() != null)
            {
                moles.Add(friend);
                friend.GetComponent<moleStatus>().controllingObject = gameObject;
            }
        }
    }

    public void GetCancelled()
    {
        GetAllMoles();
        foreach (GameObject mole in moles)
        {
            mole.GetComponent<moleStatus>().mates.Clear();
            mole.GetComponent<moleStatus>().line.SetPosition(0, new Vector3(0, 0, 0));
            mole.GetComponent<moleStatus>().line.SetPosition(1, new Vector3(0, 0, 0));
            mole.GetComponent<moleStatus>().line.SetPosition(2, new Vector3(0, 0, 0));
            mole.GetComponent<moleStatus>().hasFired = false;
            mole.GetComponent<moleStatus>().nearestFriend = null;
            mole.GetComponent<moleStatus>().giveFunnyVisual = false;
            Destroy(mole.GetComponent<moleStatus>().hitbox);
            mole.GetComponent<moleStatus>().line.material = mole.GetComponent<moleStatus>().invis;
        }
        badPositions.Clear();
        rockPositions.Clear();
        if (spawnedZap != null)
        {
            Destroy(spawnedZap);
        }
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

    void FindRockPositions()
    {
        rockPositions.Clear();
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject obstacle in obstacles)
        {
            rockPositions.Add(obstacle.transform.position);
        }
    }
}
