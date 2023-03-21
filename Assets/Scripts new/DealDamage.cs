using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float finalDamageStat;
    public float procCoeff;
    public float damageBase;
    public float damageMult;
    public float finalDamageMult = 1;
    public float finalDamageDIV = 1;
    public float critProb = 0.05f;
    public float critMult = 2;
    public GameObject owner;
    public GameObject master;

    public float massCoeff = 2;

    public bool overwriteDamageCalc;
    public bool onlyDamageOnce = true;

    public int tickInterval = 1;
    public int timer = 0;
    public float damageAmt;

    public float iFrameFac = 1;

    public bool isBulletClone = false;
    public bool isSourceBullet = false;

    public float damageToPresent;

    public int damageType;

    // Start is called before the first frame update
    void Start()
    {
        if (!overwriteDamageCalc)
        {
            damageAmt = damageBase * damageMult * finalDamageMult / finalDamageDIV;
        }

        if (gameObject.tag == "Hostile" || gameObject.tag == "Player")
        {
            owner = gameObject;
            master = GameObject.Find("bigFuckingMasterObject");
        }
        else
        {
            if (owner != null)
            {
                master = owner.GetComponent<DealDamage>().master;
            }
        }
    }

    public void CalcDamage()
    {
        if (!overwriteDamageCalc)
        {
            damageAmt = damageBase * damageMult * finalDamageMult / finalDamageDIV;
        }
    }

    void Update()
    {
        if (!overwriteDamageCalc)
        {
            damageAmt = damageBase * damageMult * finalDamageMult / finalDamageDIV;
        }

        damageToPresent = damageBase * damageMult * finalDamageMult / finalDamageDIV;
    }

    void FixedUpdate()
    {
        timer++;
        if (timer % tickInterval == 0)
        {
            finalDamageStat = damageAmt;
        }
        else
        {
            finalDamageStat = 0;
        }
    }

    //For applying any on-hit effects - sends the RollOnHit message, which is picked up by any on-hit effects THIS object has, which then apply the effect or whatever to col.gameobject.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag != col.gameObject.tag && col.gameObject.tag != "Wall" && col.gameObject.tag != "PlayerBullet" && col.gameObject.tag != "enemyBullet" && col.gameObject.GetComponent<DealDamage>() != null)
        {
            gameObject.SendMessage("RollOnHit", col.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (finalDamageStat != 0 && gameObject.tag != col.gameObject.tag && col.gameObject.tag != "Wall" && col.gameObject.tag != "PlayerBullet" && col.gameObject.tag != "enemyBullet" && col.gameObject.GetComponent<DealDamage>() != null && col.gameObject.GetComponent<HPDamageDie>().iFrames < 0)
        {
            gameObject.SendMessage("RollOnHit", col.gameObject);
        }
    }

    public void TriggerTheOnHits(GameObject whoToEffect)
    {
        gameObject.SendMessage("RollOnHit", whoToEffect);
    }
}
