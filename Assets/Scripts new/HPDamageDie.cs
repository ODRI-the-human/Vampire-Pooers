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
    public Rigidbody2D rb;
    public float iFramesTimer = 50;
    [HideInInspector] public float iFrames = 0;
    public SpriteRenderer sprite;
    public Material material;
    bool playerControlled;
    public int colorChangeTimer = 0;
    Color originalColor;
    GameObject Player;
    public GameObject XP;
    public float creepTimer = 0;

    public int perfectWaves = 0;

    GameObject master;

    GameObject poisonSplosm;

    // Start is called before the first frame update
    void Awake()
    {
        originalColor = sprite.color;
        HP = MaxHP;
        if (gameObject.tag == "Hostile")
        {
            Player = GameObject.Find("newPlayer");
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
        if (HP <= 0.49f)
        {
            if (gameObject.GetComponent<moleShit>() != null)
            {
                if (gameObject.GetComponent<moleShit>().goesFirst && gameObject.GetComponent<moleShit>().mates.Count != 0)
                {
                    GameObject bazza = gameObject.GetComponent<moleShit>().mates[0];
                    bazza.GetComponent<moleShit>().goesFirst = true;
                }
            }
            Instantiate(XP, transform.position, transform.rotation);
            Instantiate(PlayerDieAudio);
            Destroy(gameObject);
            EventManager.DeathEffects(transform.position);
        }

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }

    void FixedUpdate()
    {
        if (colorChangeTimer == 0)
        {
            sprite.color = originalColor;
        }

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }

        iFrames--;
        colorChangeTimer--;
        creepTimer--;
    }

    void OnCollisionEnter2D(Collision2D col)
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

        if (col.gameObject.tag != gameObject.tag)
        {
            if (gameObject.GetComponent<ItemEASIERTIMES>() != null && Mathf.RoundToInt(100 * (0.8f - 1f / (gameObject.GetComponent<ItemEASIERTIMES>().instances + 1f))) > Random.Range(0, 100))
            {
                //arse
            }
            else
            {
                if (playerControlled == false)
                {
                    sprite.color = Color.red;
                    colorChangeTimer = 3;
                }
                else
                {
                    if (MaxHP >= 100 && Mathf.RoundToInt(damageAmount) >= MaxHP)
                    {
                        damageAmount = MaxHP - 1;
                    }
                }
                if (iFrames < 0)
                {
                    HP -= damageAmount;
                    Instantiate(PlayerHurtAudio);
                    if (playerControlled == true)
                    {
                        iFrames = iFramesTimer;
                        perfectWaves = -1;
                    }

                    if (gameObject.GetComponent<ItemHOLYMANTIS>() != null && gameObject.GetComponent<ItemHOLYMANTIS>().timesHit > 0)
                    {
                        master.GetComponent<showDamageNumbers>().showDamage(transform.position, damageAmount - gameObject.GetComponent<ItemHOLYMANTIS>().instances * col.gameObject.GetComponent<DealDamage>().finalDamageStat / (gameObject.GetComponent<ItemHOLYMANTIS>().instances + 1), (int)DAMAGETYPES.NORMAL, isCrit);
                    }
                    else
                    {
                        master.GetComponent<showDamageNumbers>().showDamage(transform.position, damageAmount, (int)DAMAGETYPES.NORMAL, isCrit);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            perfectWaves++;
        }
    }

    void OnTriggerStay2D(Collider2D col) // just creep/orbitals lmao
    {
        float procMoment = 100f - 100f * col.gameObject.GetComponent<DealDamage>().critProb * col.gameObject.GetComponent<DealDamage>().procCoeff;
        float pringle = Random.Range(0f, 100f);
        float critMult = 1;
        bool isCrit = false;
        if (pringle > procMoment)
        {
            critMult = col.gameObject.GetComponent<DealDamage>().critMult;
            isCrit = true;
            Instantiate(CritAudio);
        }
        float damageAmount = col.gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;

        if (col.gameObject.tag == "ATGExplosion")
        {
            master.GetComponent<showDamageNumbers>().showDamage(transform.position, damageAmount, (int)DAMAGETYPES.NORMAL, isCrit);

            if (playerControlled == true)
            {
                Debug.Log((Mathf.RoundToInt(damageAmount)).ToString());
                if (MaxHP >= 100 && Mathf.RoundToInt(damageAmount) >= MaxHP)
                {
                    damageAmount = MaxHP - 1;
                }
                HP -= damageAmount;
                Instantiate(PlayerHurtAudio);
                iFrames = iFramesTimer;
                perfectWaves = -1;

            }
            if (playerControlled == false)
            {
                sprite.color = Color.red;
                colorChangeTimer = 1;
                HP -= damageAmount;
            }

            Physics2D.IgnoreCollision(col, gameObject.GetComponent<Collider2D>(), true);
        }

        if ((col.gameObject.tag == "enemyBullet" && gameObject.tag == "Player") || (col.gameObject.tag == "PlayerBullet" && gameObject.tag == "Hostile")) // otherwise xp drops would probably deal damage
        {
            if (iFrames < 0 && creepTimer < 0)
            {
                master.GetComponent<showDamageNumbers>().showDamage(transform.position, col.gameObject.GetComponent<DealDamage>().finalDamageStat, (int)DAMAGETYPES.NORMAL, isCrit);

                if (playerControlled == true)
                {
                    HP -= damageAmount;
                    Instantiate(PlayerHurtAudio);
                    iFrames = 4;
                    perfectWaves = -1;

                }
                if (playerControlled == false)
                {
                    sprite.color = Color.red;
                    colorChangeTimer = 1;
                    creepTimer = 4;
                    HP -= damageAmount;
                }
            }
        }
    }
}
