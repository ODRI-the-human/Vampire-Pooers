using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDamageDie : MonoBehaviour
{

    public float HP;
    public float MaxHP;
    public GameObject Barry63;
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

    public float damageReduction = 0;

    public int perfectWaves = 0;

    GameObject master;

    GameObject poisonSplosm;

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
        master = GameObject.Find("bigFuckingMasterObject");
    }

    void Update()
    {
        if (HP < 0.5f)
        {
            if (gameObject.GetComponent<moleShit>() != null)
            {
                if (gameObject.GetComponent<moleShit>().goesFirst && gameObject.GetComponent<moleShit>().mates.Count != 0)
                {
                    GameObject bazza = gameObject.GetComponent<moleShit>().mates[0];
                    bazza.GetComponent<moleShit>().goesFirst = true;
                }
            }
            Instantiate(XP, transform.position, Quaternion.Euler(0, 0, 0));
            Instantiate(PlayerDieAudio);
            Destroy(gameObject);
            SendMessage("ApplyOwnOnDeaths");
            EventManager.DeathEffects(transform.position);
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

    public void Hurty(float damageAmount, bool isCrit, bool playSound, float iFrameFac)
    {
        Debug.Log("shoulda taken a lil damage cunt, " + gameObject.name.ToString() + " / " + damageAmount.ToString());

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

            if (iFrames < 0 && damageAmount != 0)
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

                HP -= damageAmount;
                if (playerControlled == true)
                {
                    iFrames = iFramesTimer * iFrameFac;
                    perfectWaves = -1;
                }

                if (gameObject.GetComponent<ItemHOLYMANTIS>() != null && gameObject.GetComponent<ItemHOLYMANTIS>().timesHit > 0)
                {
                    master.GetComponent<showDamageNumbers>().showDamage(transform.position, damageAmount - gameObject.GetComponent<ItemHOLYMANTIS>().instances * damageAmount / (gameObject.GetComponent<ItemHOLYMANTIS>().instances + 1), (int)DAMAGETYPES.NORMAL, isCrit);
                }
                else
                {
                    master.GetComponent<showDamageNumbers>().showDamage(transform.position, damageAmount, (int)DAMAGETYPES.NORMAL, isCrit);
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != gameObject.tag && col.gameObject.GetComponent<DealDamage>().finalDamageStat - damageReduction >= 0)
        {
            float procMoment = 100f - 100f * col.gameObject.GetComponent<DealDamage>().critProb * col.gameObject.GetComponent<DealDamage>().procCoeff;
            float pringle = Random.Range(0f, 100f);
            float critMult = 1;
            bool isCrit = false;
            if (pringle > procMoment)
            {
                critMult = col.gameObject.GetComponent<DealDamage>().critMult;
                Instantiate(CritAudio);
                isCrit = true;
            }
            float damageAmount = col.gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;
            if (col.gameObject.GetComponent<DealDamage>().onlyDamageOnce)
            {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
            }
            Hurty(damageAmount, isCrit, true, 1);
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
                Hurty(damageAmount, isCrit, true, col.GetComponent<DealDamage>().iFrameFac);
            }
        }
    }
}
