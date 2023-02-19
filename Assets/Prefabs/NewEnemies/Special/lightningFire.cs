using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningFire : MonoBehaviour
{
    public List<float> activeAngles = new List<float>();
    public List<GameObject> hitbox = new List<GameObject>();
    public List<Vector3> hitboxPosses = new List<Vector3>();

    public GameObject zappyAudio;

    public Vector3 vecToTarget;

    float fuckAngle;

    public GameObject moleProj;

    public Sprite warn;
    public Sprite shootery;

    int currentStep = 0;
    int noExtraShots;

    GameObject owney;
    GameObject torgot;

    float timer = -50;
    public LineRenderer line;

    float prevSpeed;

    public GameObject sprongleAudio;
    public GameObject spawnedSprongleAudio;

    public void Target(GameObject currentTarget, float currentAngle, int numExtras)
    {
        prevSpeed = gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed;
        gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed = 0;

        activeAngles.Add(currentAngle);
        torgot = currentTarget;
        noExtraShots = numExtras;

        vecToTarget = torgot.transform.position - transform.position;
        vecToTarget = new Vector3(vecToTarget.x, vecToTarget.y, 0);
        GameObject Harrybo = Instantiate(moleProj, transform.position, Quaternion.Euler(0, 0, 0));
        hitbox.Add(Harrybo);
        Harrybo.transform.localScale = new Vector3(1, 1, 1);//150, 1);
        Harrybo.transform.rotation = Quaternion.LookRotation(vecToTarget) * Quaternion.Euler(0, 90, 90 + 180 * noExtraShots + (360 / 2 * Mathf.PI) * currentAngle);
        Harrybo.GetComponent<lazerMovement>().owner = gameObject;
        currentStep = 1;

        spawnedSprongleAudio = Instantiate(sprongleAudio);
    }

    void FixedUpdate()
    {
        timer--;

        if (timer == 0)
        {
            manasn(hitboxPosses);
        }
    }

    void Update()
    {
        int i = 0;

        if (currentStep == 1)
        {
            vecToTarget = torgot.transform.position - transform.position;
            vecToTarget = new Vector3(vecToTarget.x, vecToTarget.y, 0);
        }

        foreach (GameObject hitbox in hitbox)
        {
            //hitbox.transform.localScale = new Vector3(1, 1, 1);//150, 1);
            //hitbox.transform.rotation = Quaternion.LookRotation(vecToTarget) * Quaternion.Euler(0, 90, 90 + 180 * noExtraShots + (360 / 2 * Mathf.PI) * activeAngles[i]);
            //hitbox.transform.position = transform.position;
            //hitbox.transform.position += 15 * hitbox.transform.up;

            i++;
        }

        line.widthMultiplier /= 1 + 15 * Time.deltaTime;
    }

    void LockPos()
    {
        currentStep = 2;
        Invoke(nameof(Killian), 0.25f);
    }

    void Killian()
    {
        activeAngles.Clear();
        hitbox.Clear();
    }

    public void FuckingKillComputer(List<Vector3> positions, float timedr)
    {
        timer = timedr;
    }

    public void manasn(List<Vector3> positions)
    {
        Destroy(spawnedSprongleAudio);
        Instantiate(zappyAudio);
        Vector3 prevPlace = transform.position;
        line.positionCount = positions.Count;
        int currentPimple = 0;

        foreach (Vector3 place in positions)
        {
            Vector3 thisPlace = place;
            line.SetPosition(currentPimple, place);
            currentPimple++;

            for (int i = 0; i < 5; i++)
            {
                Vector3 currentPlace = Vector3.Lerp(prevPlace, thisPlace, 0.2f * i);
                Debug.Log("Current lazer check pos:" + currentPlace.ToString());
                if ((currentPlace - torgot.transform.position).magnitude < 0.5f)
                {
                    torgot.GetComponent<HPDamageDie>().Hurty(gameObject.GetComponent<DealDamage>().finalDamageStat, false, true, 1);
                }
            }

            prevPlace = thisPlace;
        }

        gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed = prevSpeed;
        line.widthMultiplier = 1.5f;
    }

    

    //public void Target(GameObject owner, GameObject target, float targetAngle, int noExtras, int shotNo)
    //{
    //    noExtraShots = noExtras;

    //    activeAngles.Add(targetAngle);

    //    owney = owner;
    //    torgot = target;

    //    Vector3 posertion = transform.position;
    //    GameObject bdjfs = Instantiate(moleProj, transform.position + new Vector3(9999, 9999, 9999) + 0.5f * (target.transform.position - transform.position), transform.rotation);
    //    hitbox.Add(bdjfs);
    //    hitbox[shotNo].transform.localScale = new Vector3(1, 150, 1);
    //    hitbox[shotNo].transform.rotation = Quaternion.Euler(0, 90, 0);
    //    hitbox[shotNo].GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
    //    hitbox[shotNo].GetComponent<DealDamage>().owner = gameObject;
    //    hitbox[shotNo].GetComponent<DealDamage>().finalDamageMult *= gameObject.GetComponent<DealDamage>().finalDamageMult;

    //    vectorMan = target.transform.position - transform.position;

    //    if (vectorMan.y > 0 && vectorMan.x > 0)
    //    {
    //        fuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
    //    }
    //    if (vectorMan.y > 0 && vectorMan.x < 0)
    //    {
    //        fuckAngle = 180 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
    //    }
    //    if (vectorMan.y < 0 && vectorMan.x < 0)
    //    {
    //        fuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 180;
    //    }
    //    if (vectorMan.y < 0 && vectorMan.x > 0)
    //    {
    //        fuckAngle = 90 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 270;
    //    }

    //    if (vectorMan.x == 0 && vectorMan.y != 0)
    //    {
    //        fuckAngle = 90;
    //    }

    //    if (vectorMan.x != 0 && vectorMan.y == 0)
    //    {
    //        fuckAngle = 0;
    //    }

    //    hitbox[shotNo].transform.Rotate(0, 90, 0, Space.World);
    //    hitbox[shotNo].transform.Rotate(0, 0, 90 + fuckAngle + activeAngles[shotNo], Space.World);
    //    hitbox[shotNo].GetComponent<DealDamage>().owner = gameObject;

    //    currentStep = 1;

    //    //Invoke(nameof(lockPos), 0.5f);
    //}

    //void lockPos()
    //{
    //    currentStep = 2;
    //    Invoke(nameof(Fire), 0.25f);
    //}

    //void Update()
    //{
    //    for (int i = 0; i < noExtraShots; i++)
    //    {
    //        if (hitbox != null && currentStep == 1)
    //        {
    //            vectorMan = torgot.transform.position - transform.position;

    //            float newFuckAngle = 0;

    //            if (vectorMan.y > 0 && vectorMan.x > 0)
    //            {
    //                newFuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
    //            }
    //            if (vectorMan.y > 0 && vectorMan.x < 0)
    //            {
    //                newFuckAngle = 180 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
    //            }
    //            if (vectorMan.y < 0 && vectorMan.x < 0)
    //            {
    //                newFuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 180;
    //            }
    //            if (vectorMan.y < 0 && vectorMan.x > 0)
    //            {
    //                newFuckAngle = 90 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 270;
    //            }

    //            if (vectorMan.x == 0 && vectorMan.y != 0)
    //            {
    //                newFuckAngle = 90;
    //            }

    //            if (vectorMan.x != 0 && vectorMan.y == 0)
    //            {
    //                newFuckAngle = 0;
    //            }

    //            float fuckAngleDiff = newFuckAngle - fuckAngle;
    //            fuckAngle = newFuckAngle + activeAngles[i];
    //            hitbox[i].transform.rotation = Quaternion.Euler(0, 0, fuckAngleDiff);

    //            Vector3 torgotterz = torgot.transform.position;
    //            torgotterz = new Vector3(torgotterz.x, torgotterz.y, 0);
    //            Vector3 owneyz = owney.transform.position;
    //            owneyz = new Vector3(owneyz.x, owneyz.y, 0);



    //            Vector3 fdsj = owneyz + 15 * (torgotterz - owneyz).normalized;
    //            hitbox[i].transform.position = new Vector3(fdsj.x, fdsj.y, 0);
    //            transformBoye = torgotterz - owneyz;
    //        }

    //        if (hitbox != null && currentStep == 2)
    //        {
    //            //hitbox[i].transform.position = owney.transform.position + 15 * hitbox[i].transform.forward;
    //        }
    //    }
    //}

    //void Fire()
    //{
    //    for (int i = 0; i < noExtraShots; i++)
    //    {
    //        hitbox[i].GetComponent<SpriteRenderer>().sprite = shootery;
    //        hitbox[i].GetComponent<enableHitbox>().enableHitboxer();
    //    }

    //    Invoke(nameof(killHitboxes), 0.25f);
    //}

    //void killHitboxes()
    //{
    //    for (int i = 0; i < noExtraShots; i++)
    //    {
    //        hitbox[i].GetComponent<enableHitbox>().FUCKINGDIE();
    //    }

    //    activeAngles.Clear();
    //    hitbox.Clear();
    //}
}
