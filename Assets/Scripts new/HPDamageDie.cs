using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDamageDie : MonoBehaviour
{

    public float HP;
    public float MaxHP;
    public AudioClip hurtAudio;
    public GameObject hurtPlane; // To replace with proper post processing.
    public AudioClip dieAudio;
    public float iFramesTimer = 50;
    [HideInInspector] public float iFrames = 0;
    public SpriteRenderer sprite;
    public Material material;
    bool playerControlled;
    public int colorChangeTimer = 0;
    Color originalColor;
    GameObject Player;
    public GameObject XP;
    [SerializeField] public float[] resistVals = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 }; // Should be a minimum of one entry for each actual real damage type.
    public string lastDamageSourceName;
    GameObject lastDamageSource;

    public bool makeKillSound = true;
    public bool showDamageNumber = true;
    public bool makeHurtSound = true;

    public float damageReduction = 0;
    public float damageDiv = 1;

    public int perfectWaves = 0;

    GameObject master;

    public List<GameObject> DOTSources = new List<GameObject>();
    public List<float> DOTDamages = new List<float>();

    // Start is called before the first frame update
    void Awake()
    {
        HP = MaxHP;
    }

    void Start()
    {
        master = EntityReferencerGuy.Instance.master;

        if (gameObject.tag == "Hostile")
        {
            Player = GameObject.Find("newPlayer");
            playerControlled = false;
            sprite = gameObject.GetComponent<SpriteRenderer>();
            originalColor = sprite.color;
        }
        else if (gameObject.tag == "enemyBullet" || gameObject.tag == "Wall")
        {
            playerControlled = false;
        }
        else
        {
            playerControlled = true;
        }
    }

    public void ApplyOwnOnDeaths()
    {
        Instantiate(XP, transform.position, Quaternion.Euler(0, 0, 0));
        if (dieAudio != null)
        {
            SoundManager.Instance.PlaySound(dieAudio);
        }
    }

    void FixedUpdate()
    {
        if (colorChangeTimer == 0 && gameObject.GetComponent<SpriteRenderer>() != null)
        {
            sprite.color = originalColor;
        }

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }

        iFrames--;
        colorChangeTimer--;

        if (iFrames % 10 == 0 && iFrames <= 0)
        {
            GameObject[] owners = new GameObject[DOTSources.Count];
            float[] damageAmounts = new float[DOTSources.Count];
            int i = 0;
            foreach (GameObject DOTSource in DOTSources)
            {
                bool doDamage = true; // This is all to make sure that damage is only dealt for the first owner and damage combo (i.e. two creep puddles, only one will hit, as the other has the same damage and owner.
                for (int j = 0; j < i; j++)
                {
                    if (owners[j] == DOTSource.GetComponent<DealDamage>().owner && damageAmounts[j] == DOTSource.GetComponent<DealDamage>().GetDamageAmount())
                    {
                        doDamage = false;
                        break;
                    }
                }

                if (doDamage)
                {
                    Hurty(DOTSource.GetComponent<DealDamage>().GetDamageAmount(), false, 0.2f, DOTSource.GetComponent<DealDamage>().damageType, false, DOTSource, false);

                    owners[i] = DOTSource.GetComponent<DealDamage>().owner;
                    damageAmounts[i] = DOTSource.GetComponent<DealDamage>().GetDamageAmount();
                }
                i++;
            }
        }
    }

    public void Hurty(float damageAmount, bool isCrit, float iFrameFac, int damageType, bool bypassIframes, GameObject objectResponsible, bool isNewAttack)
    {
        // isNewAttack is essentially for anything that isn't like a status effect - attacks for which this is true get bypassed when dodging.
        // This is needed for like lazers and stuff, which otherwise would hit even when dodging.

        // Just doing the message to show that hurty happened xd!
        string responsibleName;
        if (objectResponsible == null)
        {
            responsibleName = "Unknown (prolly status)";
        }
        else
        {
            responsibleName = objectResponsible.name.ToString();
        }

        bool doDealDamage = true;
        if (objectResponsible != null && !objectResponsible.GetComponent<DealDamage>().canDealDamage)
        {
            doDealDamage = false;
        }

        if (gameObject.GetComponent<ItemEASIERTIMES>() != null 
            && Mathf.RoundToInt(100 * (0.8f - 1f / (gameObject.GetComponent<ItemEASIERTIMES>().instances + 1f))) > Random.Range(0, 100)
            || (isNewAttack && gameObject.GetComponent<NewPlayerMovement>() != null && gameObject.GetComponent<NewPlayerMovement>().isDodging == 1))
        {
            doDealDamage = false;
        }

        if (doDealDamage)
        {
            if (playerControlled == false && gameObject.GetComponent<SpriteRenderer>() != null)
            {
                sprite.color = Color.red;
                colorChangeTimer = 3;
            }

            if ((iFrames < 0 || bypassIframes) && damageAmount != 0)
            {
                if (makeHurtSound)
                {
                    SoundManager.Instance.PlayTypicalSound((int)COMMONSNDCLPS.HITMARKER);
                    if (hurtAudio != null)
                    {
                        SoundManager.Instance.PlaySound(hurtAudio);
                    }

                    if (isCrit)
                    {
                        SoundManager.Instance.PlayTypicalSound((int)COMMONSNDCLPS.CRIT);
                    }
                }
                //Debug.Log("resistVal to this damage: " + resistVals[damageType].ToString());

                damageAmount -= damageAmount * (resistVals[damageType] / 100);
                damageAmount /= damageDiv;

                Debug.Log("damage taken, victim: " + gameObject.name.ToString() + ", responsible: " + responsibleName + ", amount: " + damageAmount.ToString());
                HP -= damageAmount;
                perfectWaves = -1;
                if (playerControlled == true)
                {
                    iFrames = iFramesTimer * iFrameFac;
                    Instantiate(hurtPlane);
                }
                else
                {
                    //iFrames = 1;
                }

                //if (gameObject.GetComponent<ItemHOLYMANTIS>() != null && gameObject.GetComponent<ItemHOLYMANTIS>().timesHit > 0)
                //{
                //    damageAmount = damageAmount - gameObject.GetComponent<ItemHOLYMANTIS>().instances * damageAmount / (gameObject.GetComponent<ItemHOLYMANTIS>().instances + 1);
                //}

                if (showDamageNumber)
                {
                    master.GetComponent<showDamageNumbers>().showDamage(transform.position + new Vector3(0, 0, -2 - transform.position.z), damageAmount, damageType, isCrit);
                }

                if (gameObject.GetComponent<ItemHolder2>() != null) // Only does the following if it's an actual player or enemy or such (no rocks allowed)
                {
                    gameObject.GetComponent<ItemHolder2>().OnHurts();
                    if (objectResponsible != null)
                    {
                        lastDamageSource = objectResponsible.GetComponent<DealDamage>().owner;
                        lastDamageSourceName = lastDamageSource.ToString();
                        lastDamageSource.GetComponent<ItemHolder2>().OnHits(gameObject, objectResponsible);
                        objectResponsible.GetComponent<ItemHolder2>().OnHits(gameObject, objectResponsible);

                        if (objectResponsible.GetComponent<ApplyAttackModifiers>() != null)
                        {
                            objectResponsible.GetComponent<ApplyAttackModifiers>().ModifierOnHits(gameObject, objectResponsible.GetComponent<DealDamage>().owner);
                        }
                    }
                }
            }
        }

        if (HP <= 0)
        {
            SendMessage("ApplyOwnOnDeaths");
            if (lastDamageSource != null && gameObject.tag == "Hostile") // otherwise it gets very funny
            {
                EntityReferencerGuy.Instance.GameManager.GetComponent<Director>().OnEnemiesKilled();
                lastDamageSource.SendMessage("ApplyItemOnDeaths", gameObject); // Calls the on-kill effects on the object responsible for the kill.
            }

            if (gameObject.tag == "Player")
            {
                EntityReferencerGuy.Instance.master.SendMessage("ApplyItemOnDeaths", gameObject); // Calls the on-kill effects on the object responsible for the kill.
            }

            //gameObject.GetComponent<Attack>().bulletPool.Clear();
            Destroy(gameObject);
            //EventManager.DeathEffects(transform.position);
        }
    }

    public void ApplyItemOnDeaths()
    {
        //please stop making errors
    }

    public void OnHurtEffects()
    {
        //shut the fuck up
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<DealDamage>() != null && col.gameObject.tag != gameObject.tag)// && col.gameObject.GetComponent<DealDamage>().finalDamageStat - damageReduction >= 0)
        {
            //float critChance = 100f * col.gameObject.GetComponent<DealDamage>().critProb;
            //int numCrits = gameObject.GetComponent<DealDamage>().ChanceRoll(critChance, col.gameObject, -5);
            //float critMult = 1;
            //bool isCrit = false;

            //for (int i = 0; i < numCrits; i++)
            //{
            //    critMult *= 2;
            //    Instantiate(CritAudio);
            //    isCrit = true;
            //}

            //float damageAmount = col.gameObject.GetComponent<DealDamage>().finalDamageStat * critMult;

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
            col.gameObject.GetComponent<DealDamage>().CalculateDamage(gameObject, col.gameObject);
            //Hurty(damageAmount, isCrit, true, 1, col.gameObject.GetComponent<DealDamage>().damageType, false, col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<DealDamage>() != null)
        {
            if (col.gameObject.GetComponent<DealDamage>().onlyDamageOnce)
            {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
                
                col.gameObject.GetComponent<DealDamage>().CalculateDamage(gameObject, col.gameObject);
            }
            else
            {
                DOTSources.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<DealDamage>() != null)
        {
            if (!col.gameObject.GetComponent<DealDamage>().onlyDamageOnce)
            {
                DOTSources.Remove(col.gameObject);
            }
        }
    }
}