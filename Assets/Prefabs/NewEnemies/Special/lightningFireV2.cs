//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class lightningFireV2 : MonoBehaviour
//{
//    public List<float> activeAngles = new List<float>();

//    public Vector3 vecToTarget;

//    float fuckAngle;

//    public GameObject moleProj;
//    public Material mainMaterial;

//    int currentStep = 0;
//    int noExtraShots;

//    GameObject owney;
//    GameObject torgot;

//    float timer = -50;

//    int LayerPlayer;
//    int LayerEnemy;

//    public GameObject sprongleAudio;
//    public GameObject spawnedSprongleAudio;
//    bool prevKnockbackSetting;

//    void Start()
//    {
//        LayerPlayer = LayerMask.NameToLayer("PlayerBullets");
//        LayerEnemy = LayerMask.NameToLayer("Enemy Bullets");
//    }

//    public IEnumerator Target(Vector3 targetPos, float currentAngle, int numExtras)
//    {
//        activeAngles.Add(currentAngle);
//        noExtraShots = numExtras;

//        vecToTarget = targetPos;// - transform.position;

//        if (gameObject.tag == "Hostile")
//        {
//            vecToTarget = targetPos - transform.position;
//            gameObject.GetComponent<NewPlayerMovement>().speedMult = 0;
//            prevKnockbackSetting = gameObject.GetComponent<NewPlayerMovement>().recievesKnockback;
//            gameObject.GetComponent<NewPlayerMovement>().recievesKnockback = false;
//            gameObject.GetComponent<NewPlayerMovement>().knockBackVector = Vector2.zero;
//        }

//        vecToTarget = new Vector3(vecToTarget.x, vecToTarget.y, 0);
//        GameObject Harrybo = Instantiate(moleProj, transform.position, Quaternion.Euler(0, 0, 0));
//        Harrybo.transform.localScale = new Vector3(1, 1, 1);//150, 1);
//        Harrybo.transform.rotation = Quaternion.LookRotation(vecToTarget) * Quaternion.Euler(0, 90, 90 + (180 / Mathf.PI) * currentAngle); //90 + 180 * noExtraShots + 
//        Harrybo.GetComponent<checkAllLazerPositions>().owner = gameObject;
//        currentStep = 1;

//        //Debug.Log(vecToTarget.ToString());

//        if (gameObject.tag == "Player" || gameObject.tag == "PlayerBullet")
//        {
//            Harrybo.tag = "PlayerBullet";
//            Harrybo.layer = LayerPlayer;
//            Harrybo.GetComponent<checkAllLazerPositions>().shoot = mainMaterial;
//        }

//        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
//        {
//            Harrybo.tag = "enemyBullet";
//            Harrybo.layer = LayerEnemy;
//            Harrybo.GetComponent<checkAllLazerPositions>().actuallyHit = false;
//            Harrybo.GetComponent<checkAllLazerPositions>().lineRandPos = 0;
//        }

//        Harrybo.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
//        Harrybo.GetComponent<DealDamage>().finalDamageMult *= gameObject.GetComponent<DealDamage>().finalDamageMult;
//        Harrybo.GetComponent<DealDamage>().owner = gameObject;
//        Harrybo.GetComponent<checkAllLazerPositions>().master = gameObject.GetComponent<DealDamage>().master;
//        Harrybo.GetComponent<DealDamage>().damageAdd = gameObject.GetComponent<Attack>().damageBonus; // applies converter damage bonus to bullets

//        if (gameObject.tag == "Hostile")
//        {
//            yield return new WaitForSecondsRealtime(0.8f);
//            Destroy(Harrybo);
//        }

//        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
//        {
//            GameObject Darrenbo = Instantiate(moleProj, transform.position, Quaternion.Euler(0, 0, 0));
//            Darrenbo.GetComponent<checkAllLazerPositions>().shoot = mainMaterial;
//            Darrenbo.transform.localScale = new Vector3(1, 1, 1);//150, 1);
//            Darrenbo.transform.rotation = Quaternion.LookRotation(vecToTarget) * Quaternion.Euler(0, 90, 90 + (180 / Mathf.PI) * currentAngle); //90 + 180 * noExtraShots + 
//            Darrenbo.GetComponent<checkAllLazerPositions>().owner = gameObject;
//            Darrenbo.tag = "enemyBullet";
//            Darrenbo.layer = LayerEnemy;
//            Darrenbo.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
//            Darrenbo.GetComponent<DealDamage>().finalDamageMult *= gameObject.GetComponent<DealDamage>().finalDamageMult;
//            Darrenbo.GetComponent<DealDamage>().owner = gameObject;
//            Darrenbo.GetComponent<checkAllLazerPositions>().master = gameObject.GetComponent<DealDamage>().master;
//            Darrenbo.GetComponent<DealDamage>().damageAdd = gameObject.GetComponent<Attack>().damageBonus;

//            gameObject.GetComponent<NewPlayerMovement>().speedMult = 1;

//            gameObject.GetComponent<NewPlayerMovement>().recievesKnockback = prevKnockbackSetting;
//        }

//        if (spawnedSprongleAudio != null)
//        {
//            Destroy(spawnedSprongleAudio);
//        }
//        spawnedSprongleAudio = Instantiate(sprongleAudio);
//    }
//}