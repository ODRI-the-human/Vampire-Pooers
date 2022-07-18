using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Movement : MonoBehaviour
{

    [HideInInspector] public static Player_Movement instance; //Static refrence for anyone who wants access

    public float moveSpeedInitial = 5f;
    float moveSpeed;
    public float shotSpeed = 5f;

    public Animator animator;

    public Rigidbody2D rb;
    Rigidbody2D bulletRB;
    public GameObject PlayerBullet;
    public GameObject Player;
    public GameObject Barry63;
    public GameObject PlayerShootAudio;
    public GameObject PlayerHurtAudio;
    public GameObject PlayerDieAudio;
    public GameObject PlayerXPAudio;
    public GameObject PlayerHPAudio;
    public GameObject creep;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI DMGText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI itemScreenText;

    public float initialFireTimerLength = 200f;
    float fireTimerLength;
    float fireTimer = 0f;
    int maxHPInitial = 100;
    int maxHP;
    public int HP;
    int noExtraShots = 0;
    public float damageStat = 50;
    public float damageMult = 1;
    float converterDamageMult = 0;
    int iFramesTimer = 50;
    int iFrames = 0;
    int XP = 0;
    Vector2 playerPos;
    Vector2 mousePos;
    Vector2 vectorToMouse;
    Vector2 newShotVector;
    float currentAngle;
    float level = 1f;
    int itemAppeared;
    int itemChosen;
    public int levelConstantMul = 1;
    float wapantTimer = 0;
    public int noWapants = 0;
    public float wapantTimerLength;
    public float initialWapantLength = 300f;
    public GameObject wapantCircle;
    int mantisCharges = 0;
    int mantisInstances = 0;
    int damageReduction;
    int converterInstances = 0;
    int easierTimesInstances = 0;
    float avoidsDamage;
    public int stopwatchInstances = 0;
    public int bounceInstances = 0;
    float finalDamageMult = 1;
    int martyInstances = 0;
    int martyCounter = 0;
    int numberOfMarties = 0;
    public float trueDamageValue;
    public int pierceInstances = 0;
    int creepInstances = 0;
    int creepTimer = 0;

    private Vector2 moveDirection;
    public List<int> itemsHeld = new List<int>();

    void Awake() //start is proberly fine aswell, better safe than pooey (Issac)
    {
        if (instance == null)
            instance = this; //this is the instance
        else
            Destroy(this); //should never be multiple instances
    }

    void Start()
    {
        UpdateStats();
        HP = maxHP;
        converterDamageMult = 0;
        SetStatsText();
    }

    void SetStatsText()
    {
        HPText.text = "HP: " + HP.ToString() + "/" + maxHP.ToString();
        XPText.text = "XP: " + XP.ToString();
        DMGText.text = "DMG: " + ((damageStat)*(damageMult+converterDamageMult)*finalDamageMult).ToString();
        LevelText.text = "Level: " + level.ToString();
    }

    void ItemScreen()
    {
        //for (int i = 0; i < 3; i++)
        //{
        itemAppeared = (int)Random.Range(-0.5f, 8.5f);
        itemScreenText.text = ((ITEMLIST)itemAppeared).ToString();
        itemsHeld.Add(itemAppeared);
        UpdateStats();
        SetStatsText();
        //}
    }

    void UpdateStats()
    {
        maxHP = maxHPInitial;
        damageStat = 50;
        damageMult = 1;
        fireTimerLength = initialFireTimerLength;
        noExtraShots = 0;
        noWapants = 0;
        wapantTimerLength = initialWapantLength;
        moveSpeed = moveSpeedInitial;
        mantisInstances = 0;
        mantisCharges = mantisInstances;
        converterInstances = 0;
        easierTimesInstances = 0;
        stopwatchInstances = 0;
        finalDamageMult = 1;
        martyInstances = 0;
        pierceInstances = 0;
        creepInstances = 0;

        // applying stat ups
        foreach (int item in itemsHeld)
        {
            switch (item)
            {
                case (int)ITEMLIST.HP25:
                    maxHP += 25;
                    break;
                case (int)ITEMLIST.HP50:
                    maxHP += 50;
                    break;
                case (int)ITEMLIST.DMGADDPT5:
                    damageStat += 25;
                    break;
                case (int)ITEMLIST.DMGMLT2:
                    damageMult += 1;
                    break;
                case (int)ITEMLIST.FIRERATE:
                    fireTimerLength /= 1.15f;
                    break;
                case (int)ITEMLIST.SOY:
                    fireTimerLength /= 5;
                    finalDamageMult /= 3;
                    break;
                case (int)ITEMLIST.MORESHOT:
                    noExtraShots += 1;
                    break;
                case (int)ITEMLIST.WAPANT:
                    noWapants += 1;
                    wapantTimerLength /= 1.5f;
                    break;
                case (int)ITEMLIST.HOLYMANTIS:
                    mantisInstances += 1;
                    mantisCharges = mantisInstances;
                    break;
                case (int)ITEMLIST.CONVERTER:
                    converterInstances++;
                    //Debug.Log("Converters: " + converterInstances.ToString());
                    break;
                case (int)ITEMLIST.EASIERTIMES:
                    easierTimesInstances++;
                    break;
                case (int)ITEMLIST.STOPWATCH:
                    stopwatchInstances++;
                    break;
                case (int)ITEMLIST.BOUNCY:
                    bounceInstances++;
                    break;
                case (int)ITEMLIST.FOURDIRMARTY:
                    martyInstances++;
                    break;
                case (int)ITEMLIST.PIERCING:
                    pierceInstances++;
                    break;
                case (int)ITEMLIST.CREEP:
                    creepInstances++;
                    break;
            }

            converterDamageMult += ((0.03f) * (1 - HP / maxHP))*converterInstances;
            Debug.Log(converterDamageMult.ToString());
            trueDamageValue = (damageStat) * (damageMult + converterDamageMult) * finalDamageMult;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        // firing the FUNNY weapon
        if (Input.GetButton("Fire1"))
        {
            if (fireTimer < 0)
            {
                Vector3 mousePos3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.x = mousePos3.x;
                mousePos.y = mousePos3.y;
                Vector3 playerPos3 = gameObject.transform.position;
                playerPos.x = playerPos3.x;
                playerPos.y = playerPos3.y;
                vectorToMouse = (mousePos - playerPos).normalized;
                //firing a number of bullets depending on how many extra shots the player has
                martyCounter++;
                for (int i = -1; i < noExtraShots; i++)
                {
                    GameObject newObject = Instantiate(PlayerBullet, transform.position, transform.rotation) as GameObject;
                    newObject.transform.localScale = new Vector3(trueDamageValue*0.0015f+.45f, trueDamageValue * 0.0015f+.45f, trueDamageValue * 0.0015f+.45f);
                    bulletRB = newObject.GetComponent<Rigidbody2D>();
                    currentAngle = 0.3f * (0.5f * noExtraShots - i - 1);
                    newShotVector = new Vector2(vectorToMouse.x * Mathf.Cos(currentAngle) - vectorToMouse.y * Mathf.Sin(currentAngle), vectorToMouse.x * Mathf.Sin(currentAngle) + vectorToMouse.y * Mathf.Cos(currentAngle));
                    bulletRB.velocity = new Vector2(newShotVector.x * shotSpeed, newShotVector.y * shotSpeed);
                }

                if (martyCounter * (1.5f*martyInstances) > 6)
                {
                    martyCounter = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        GameObject newObject = Instantiate(PlayerBullet, transform.position, transform.rotation) as GameObject;
                        newObject.transform.localScale = new Vector3(trueDamageValue * 0.0015f + .4f, trueDamageValue * 0.0015f + .4f, trueDamageValue * 0.0015f + .4f);
                        bulletRB = newObject.GetComponent<Rigidbody2D>();
                        currentAngle = (Mathf.PI / 4)*Mathf.Sin(numberOfMarties*Mathf.PI/2) + i * Mathf.PI / 2;
                        newShotVector = new Vector2(Mathf.Cos(currentAngle) - Mathf.Sin(currentAngle), Mathf.Sin(currentAngle) + Mathf.Cos(currentAngle));
                        bulletRB.velocity = new Vector2(newShotVector.x * shotSpeed, newShotVector.y * shotSpeed);
                    }
                    numberOfMarties++;
                }
                Instantiate(PlayerShootAudio);
                fireTimer = fireTimerLength;
            }
        }

        // >dies
        if (HP <= 0)
        {
            Destroy(Player);
            //Debug.Log("Owned Lu zer");
            Instantiate(Barry63, new Vector3(0, 0, -1), new Quaternion(1, Mathf.PI, 0, 0));
            Instantiate(PlayerDieAudio);
        }

        // animation
        //animator.SetFloat("horizontal", Mathf.Abs(moveDirection.magnitude));

        if (Input.GetAxisRaw("Horizontal") < -0.001)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1);
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") > 0.001)
            {
                gameObject.transform.localScale = new Vector3(-1f, 1f, 1);
            }
        }

        Debug.Log("Damage: " + ((damageStat) * (damageMult + converterDamageMult) * finalDamageMult).ToString() + " damageStat: " + damageStat.ToString() + " damageMult: " + damageMult.ToString() + " converterDamageMult: " + converterDamageMult.ToString() + " finalDamageMult: " + finalDamageMult.ToString());
        trueDamageValue = (damageStat) * (damageMult + converterDamageMult) * finalDamageMult; // Dunno why but if I put this calculation in updatestats it gives a damage value of 0 so it's here for now
    }

    void FixedUpdate()
    {
        // movement
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        // reducing cooldown timer of weapon
        fireTimer -= 1;

        // reducing iframes
        iFrames -= 1;

        wapantTimer--;

        if (noWapants > 0)
        {
            if (wapantTimer < 1)
            {
                wapantTimer = wapantTimerLength;
                Instantiate(wapantCircle, transform.position + new Vector3(0,0,0.5f), transform.rotation);
            }
        }

        creepTimer--;
        if (creepInstances > 0)
        {
            if (creepTimer < 1)
            {
                creepTimer = 5;
                GameObject newObject5 = Instantiate(creep, transform.position + new Vector3(0, 0, 0.5f), transform.rotation) as GameObject;
                newObject5.transform.localScale = new Vector3(6 + 2*creepInstances, 6 + 2 * creepInstances, 6 + 2 * creepInstances);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        {
            // if player has skill issue and consequently gets hit (bad at game)
            if (col.gameObject.tag == "Hostile" || col.gameObject.tag == "enemyBullet")
            {
                if (iFrames < 0)
                {
                    if (easierTimesInstances > 0)
                    {
                        avoidsDamage = Random.Range(0, 7 + 3 * Mathf.Log(easierTimesInstances + 3.5f, 3));
                    }
                    else
                    {
                        avoidsDamage = 0;
                    }

                    if (avoidsDamage < 7)
                    {
                        if (mantisCharges > 0)
                        {
                            damageReduction = Mathf.RoundToInt(30 * Mathf.Pow(1.05f, 0.5f * mantisCharges));
                        }
                        else
                        {
                            damageReduction = 0;
                        }
                        HP -= 50 - damageReduction;
                        mantisCharges--;
                        //Debug.Log("Collided");
                        Instantiate(PlayerHurtAudio);
                        SetStatsText();
                    }
                    iFrames = iFramesTimer;
                }
            }
        }


        // picking up xp
        if (col.gameObject.tag == "XP")
        {
            XP += 10;
            Instantiate(PlayerXPAudio);
            // levelling up
            if (XP >= 75*level + levelConstantMul * Mathf.Pow(1.5f, 1.1f * level)) 
            {
                level += 1;
                ItemScreen();
            }
            SetStatsText();
        }

        // picking up hp
        if (col.gameObject.tag == "HP")
        {
            HP += 25;
            if (HP > maxHP)
            {
                HP = maxHP;
            }
            Instantiate(PlayerHPAudio);
            SetStatsText();
        }

        // selecting an item
        if (col.gameObject.tag == "item")
        {
            itemAppeared = col.GetComponent<itemPedestal>().itemChosen;
            itemsHeld.Add(itemAppeared);
            itemScreenText.text = ((ITEMLIST)itemAppeared).ToString();
            UpdateStats();
            SetStatsText();
        }
    }
}
