using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float finalDamageStat;
    public float procCoeff;
    public float damageBase;
    public float damageAdd;
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

    public float damageToPresent;

    public int damageType;
    public List<int> procChainIndexes = new List<int>();

    // Start is called before the first frame update
    void Awake()
    {
        CalcDamage();
    }

    void Start()
    {
        master = EntityReferencerGuy.Instance.master;

        if (gameObject.tag == "Hostile" || gameObject.tag == "Player")
        {
            owner = gameObject;
        }

        CalcDamage();
    }

    public void DetermineShotRolls()
    {
        procChainIndexes.Clear();
    }

    public void CalcDamage()
    {
        if (!overwriteDamageCalc)
        {
            damageAmt = damageBase;// * damageMult * finalDamageMult / finalDamageDIV;
            damageAmt = damageBase * finalDamageMult;
        }
    }

    void Update()
    {
        CalcDamage();

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
    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.GetComponent<HPDamageDie>() != null && procCoeff != 0 && finalDamageStat != 0 && gameObject.tag != col.gameObject.tag && col.gameObject.tag != "Wall" && col.gameObject.GetComponent<HPDamageDie>().iFrames < 0)
    //    {
    //        SendRollOnHits(col.gameObject);
    //    }
    //}

    //void OnTriggerStay2D(Collider2D col)
    //{
    //    Debug.Log("erm globule");
    //    //if (col.gameObject.GetComponent<HPDamageDie>() != null && procCoeff != 0 && finalDamageStat != 0 && gameObject.tag != col.gameObject.tag && col.gameObject.tag != "Wall" && col.gameObject.GetComponent<HPDamageDie>().iFrames < 0)
    //    //{
    //    SendRollOnHits(col.gameObject);
    //    //}
    //}

    //public void TriggerTheOnHits(GameObject whoToEffect)
    //{
    //    SendRollOnHits(whoToEffect);
    //}

    public void SendRollOnHits(GameObject victim)
    {
        if (procCoeff > 0)
        {
            owner.SendMessage("RollOnHit", new GameObject[] { victim, gameObject });
            gameObject.SendMessage("RollOnHit", new GameObject[] { victim, gameObject });
        }
    }

    public void RollOnHit(GameObject[] gameObjects)
    {
        //erm nothing, literally just here to get the sendmessages to shut the fuck up about no recievers!
    }

    public int ChanceRoll(float value, GameObject source, int procItemIndex) // Items that are rolling for a chance-based thing use this. 
    {
        if (gameObject.tag == "Hostile" || gameObject.tag == "Player") // The player and shit shouldn't have proc indexes anyway
        {
            procChainIndexes.Clear();
        }

        bool scriptIsUsed = false; // This stores whether the current script doing the roll has already been used in the proc chain.
        List<int> procIndexes = source.GetComponent<DealDamage>().procChainIndexes;

        // Checks if the item index calling the chance roll has already been used in this proc chain.
        for (int i = 0; i < procIndexes.Count; i++)
        {
            if (procIndexes[i] == procItemIndex)
            {
                scriptIsUsed = true;
            }
        }

        int numberOfProcs = 0;

        if (!scriptIsUsed)
        {
            float percentChance = value * source.GetComponent<DealDamage>().procCoeff;
            Debug.Log("erm percentage chance do be: " + percentChance.ToString() + ", value: " + value.ToString());

            for (int i = 0; i < 20; i++)
            {
                float procMoment = 100f - percentChance;
                float pringle = Random.Range(0f, 100f);

                if (pringle > procMoment)
                {
                    numberOfProcs++;
                }

                if (percentChance > 100f)
                {
                    percentChance -= 100f;
                }
                else
                {
                    break;
                }
            }
        }

        return numberOfProcs;
    }

    //public void RollOnHit(GameObject[] objects)
    //{
    //    GameObject victim = objects[0];
    //    GameObject source = objects[1];
    //    Debug.Log(victim.ToString() + " / " + source.ToString());
    //}
}