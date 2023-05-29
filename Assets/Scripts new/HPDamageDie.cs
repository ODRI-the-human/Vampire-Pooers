using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDamageDie : MonoBehaviour
{

    public float HP;
    public float MaxHP;
    public GameObject PlayerDieAudio;
    public GameObject PlayerHurtAudio;
    public GameObject CritAudio;
    public float iFramesTimer = 50;
    [HideInInspector] public float iFrames = 0;
    public SpriteRenderer sprite;
    public Material material;
    bool playerControlled;
    public int colorChangeTimer = 0;
    Color originalColor;
    GameObject Player;
    public GameObject XP;
    [System.NonSerialized] public float[] resistVals = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 }; // Should be a minimum of one entry for each actual real damage type.
    public string lastDamageSourceName;
    GameObject lastDamageSource;

    public bool makeKillSound = true;

    public float damageReduction = 0;
    public float damageDiv = 1;

    public int perfectWaves = 0;

    GameObject master;

    // Start is called before the first frame update
    void Awake()
    {
        HP = MaxHP;
        if (gameObject.tag == "Hostile")
        {
            Player = GameObject.Find("newPlayer");
            playerControlled = false;
            sprite = gameObject.GetComponent<SpriteRenderer>();
            originalColor = sprite.color;
        }
        else if (gameObject.tag == "enemyBullet")
        {
            playerControlled = false;
        }
        else
        {
            playerControlled = true;
        }
    }

    void Start()
    {
        master = EntityReferencerGuy.Instance.master;
    }

    public void ApplyOwnOnDeaths()
    {
        Instantiate(XP, transform.position, Quaternion.Euler(0, 0, 0));
        if (makeKillSound)
        {
            Instantiate(PlayerDieAudio);
        }
    }

    void Update()
    {
        if (HP <= 0)
        {
            SendMessage("ApplyOwnOnDeaths");
            if (lastDamageSource != null) // otherwise it gets very funny
            {
                if (gameObject.tag == "Hostile")
                {
                    lastDamageSource.SendMessage("ApplyItemOnDeaths", gameObject); // Calls the on-kill effects on the object responsible for the kill.
                }
                else
                {
                    EntityReferencerGuy.Instance.master.SendMessage("ApplyItemOnDeaths", gameObject);
                    EntityReferencerGuy.Instance.camera.SendMessage("CheckAlivePlayers");
                }
            }

            Destroy(gameObject);
            //EventManager.DeathEffects(transform.position);
        }

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }

    void FixedUpdate()
    {
        if (colorChangeTimer == 0 && gameObject.GetComponent<SpriteRenderer>() != null)
        {
            sprite.color = originalColor;
        }

        iFrames--;
        colorChangeTimer--;
    }

    public void Hurty(float damageAmount, bool isCrit, bool playSound, float iFrameFac, int damageType, bool bypassIframes, GameObject objectResponsible)
    {
        //Debug.Log("shoulda taken a lil damage cunt, " + gameObject.name.ToString() + " / " + damageAmount.ToString());
        //Debug.Log(resistVals[damageType].ToString());

        if (gameObject.GetComponent<ItemEASIERTIMES>() != null && Mathf.RoundToInt(100 * (0.8f - 1f / (gameObject.GetComponent<ItemEASIERTIMES>().instances + 1f))) > Random.Range(0, 100))
        {
            //arse
        }
        else
        {
            if (playerControlled == false && gameObject.GetComponent<SpriteRenderer>() != null)
            {
                sprite.color = Color.red;
                colorChangeTimer = 3;
            }

            if ((iFrames < 0 || bypassIframes) && damageAmount != 0)
            {
                if (playSound)
                {
                    GameObject hurteo = Instantiate(PlayerHurtAudio, new Vector3(0,0,-5), transform.rotation);
                    hurteo.GetComponent<AudioSource>().volume /= 1.5f;
                }

                if (isCrit)
                {
                    GameObject hurtzeo = Instantiate(CritAudio, new Vector3(0, 0, -5), transform.rotation);
                    if (damageAmount < gameObject.GetComponent<DealDamage>().damageAmt)
                    {
                        hurtzeo.GetComponent<AudioSource>().volume /= 1.5f;
                    }
                }

                damageAmount -= damageAmount * (resistVals[damageType] / 100);
                damageAmount /= damageDiv;

                HP -= damageAmount;
                if (playerControlled == true)
                {
                    iFrames = iFramesTimer * iFrameFac;
                    perfectWaves = -1;
                }

                //if (gameObject.GetComponent<ItemHOLYMANTIS>() != null && gameObject.GetComponent<ItemHOLYMANTIS>().timesHit > 0)
                //{
                //    damageAmount = damageAmount - gameObject.GetComponent<ItemHOLYMANTIS>().instances * damageAmount / (gameObject.GetComponent<ItemHOLYMANTIS>().instances + 1);
                //}

                master.GetComponent<showDamageNumbers>().showDamage(transform.position, damageAmount, damageType, isCrit);
                SendMessage("OnHurtEffects");

                if (objectResponsible != null)
                {
                    lastDamageSource = objectResponsible.GetComponent<DealDamage>().owner;
                    lastDamageSourceName = lastDamageSource.ToString();
                    objectResponsible.SendMessage("SendRollOnHits", gameObject);
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<DealDamage>() != null && col.gameObject.tag != gameObject.tag && col.gameObject.GetComponent<DealDamage>().finalDamageStat - damageReduction >= 0)
        {
            float critChance = 100f * col.gameObject.GetComponent<DealDamage>().critProb;
            int numCrits = gameObject.GetComponent<DealDamage>().ChanceRoll(critChance, col.gameObject, -5);
            float critMult = 1;
            bool isCrit = false;

            for (int i = 0; i < numCrits; i++)
            {
                critMult *= 2;
                Instantiate(CritAudio);
                isCrit = true;
            }

            float damageAmount = col.gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;

            //float procMoment = 100f - 100f * col.gameObject.GetComponent<DealDamage>().critProb * col.gameObject.GetComponent<DealDamage>().procCoeff;
            //float pringle = Random.Range(0f, 100f);
            //bool isCrit = false;
            //if (pringle > procMoment)
            //{
            //    critMult = col.gameObject.GetComponent<DealDamage>().critMult;
            //    Instantiate(CritAudio);
            //    isCrit = true;
            //}
            //float damageAmount = col.gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;
            if (col.gameObject.GetComponent<DealDamage>().onlyDamageOnce)
            {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
            }
            Hurty(damageAmount, isCrit, true, 1, col.gameObject.GetComponent<DealDamage>().damageType, false, col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            perfectWaves++;
        }
    }

    void OnTriggerStay2D(Collider2D col) // just creep/orbitals/sawshot/etc. lmao
    {
        if (col.gameObject.GetComponent<DealDamage>() != null)
        {
            if (col.gameObject.tag != gameObject.tag && col.gameObject.GetComponent<DealDamage>().finalDamageStat != 0)
            {
                float procMoment = 100f - 100f * col.gameObject.GetComponent<DealDamage>().critProb * col.gameObject.GetComponent<DealDamage>().procCoeff;
                float pringle = Random.Range(0f, 100f);
                float critMult = 1;
                bool isCrit = false;
                if (pringle > procMoment)
                {
                    critMult = col.gameObject.GetComponent<DealDamage>().critMult;
                    isCrit = true;
                }
                float damageAmount = col.gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;
                if (col.gameObject.GetComponent<DealDamage>().onlyDamageOnce)
                {
                    Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
                }

                // Calculating damage falloff when hit by an explosoin (real)
                if (col.gameObject.GetComponent<explosionBONUSSCRIPTWOW>() != null)
                {
                    Vector3 vecFromCtr = (col.transform.position - transform.position) / (2.65f * col.transform.localScale.x);
                    float fracFromCtr = new Vector3(vecFromCtr.x, vecFromCtr.y, 0).magnitude;
                    damageAmount *= Mathf.Clamp(1.5f * (-3.33f * Mathf.Log(fracFromCtr + 1) + 1), 0, 1);
                }

                Hurty(damageAmount, isCrit, true, col.GetComponent<DealDamage>().iFrameFac, col.gameObject.GetComponent<DealDamage>().damageType, false, col.gameObject);
            }
        }
    }
}
