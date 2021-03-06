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
    public TextMeshProUGUI FirerateText;
    public SpriteRenderer sprite;

    public float initialFireTimerLength = 200f;
    float fireTimerLength;
    float fireTimer = 0f;
    float maxHPInitial = 100;
    float maxHP;
    public float HP;
    int noExtraShots = 0;
    public float damageStat = 50;
    public float damageMult = 1;
    float converterDamageMult = 0;
    float iFramesTimer = 50;
    float iFrames = 0;
    int XP = 0;
    Vector2 playerPos;
    Vector2 mousePos;
    Vector2 vectorToMouse;
    Vector2 newShotVector;
    float currentAngle;
    float level = 0f;
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
    float dodgeTimer;
    float dodgeTimerLength = 15;
    int isDodging = 0;
    public GameObject dodgeAudio;
    public int dodgeSplosionInstances = 0;
    public GameObject dodgeSplosion;
    int betterDodgeInstances = 0;
    public GameObject Orbital;
    public GameObject Orbital2;
    int orbital1Instances = 0;
    public int orbital2Instances = 0;
    float orbital2Timer = 0;
    public int splitInstances = 0;
    float shotAngleCoeff = 1;
    public int explodeInstances = 0;
    bool normieBullets = true;
    public GameObject swordHitbox;
    public GameObject Shield;
    int swordTimer = 50;
    public int contactInstances = 0;

    private Vector2 moveDirection;
    public List<int> itemsHeld = new List<int>();
    int weaponHeld;

    void Awake() //start is proberly fine aswell, better safe than pooey (Issac)
    {
        if (instance == null)
            instance = this; //this is the instance
        else
            Destroy(this); //should never be multiple instances

        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        weaponHeld = (int)WEAPONS.SWORD;
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
        FirerateText.text = "Fire delay: " + fireTimerLength.ToString();
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
        maxHP = maxHPInitial * (1 + 0.03f * level);
        damageStat = 50 * (1 + 0.03f * level);
        damageMult = 1;
        fireTimerLength = initialFireTimerLength / (1 + 0.03f * level);
        noExtraShots = 0;
        noWapants = 0;
        wapantTimerLength = initialWapantLength;
        moveSpeed = moveSpeedInitial * (1 + 0.03f * level);
        mantisInstances = 0;
        mantisCharges = mantisInstances;
        converterInstances = 0;
        easierTimesInstances = 0;
        stopwatchInstances = 0;
        finalDamageMult = 1;
        martyInstances = 0;
        pierceInstances = 0;
        creepInstances = 0;
        dodgeSplosionInstances = 0;
        betterDodgeInstances = 0;
        orbital1Instances = 0;
        orbital2Instances = 0;
        splitInstances = 0;
        shotAngleCoeff = 1;
        explodeInstances = 0;
        contactInstances = 0;
        normieBullets = true;

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
                    fireTimerLength /= 1.25f;
                    break;
                case (int)ITEMLIST.SOY:
                    fireTimerLength /= 5;
                    finalDamageMult /= 4;
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
                case (int)ITEMLIST.DODGESPLOSION:
                    dodgeSplosionInstances++;
                    break;
                case (int)ITEMLIST.BETTERDODGE:
                    betterDodgeInstances++;
                    break;
                case (int)ITEMLIST.ORBITAL1:
                    orbital1Instances++;
                    if (GameObject.FindGameObjectsWithTag("OrbitalContact").Length < orbital1Instances)
                    {
                        GameObject OrbitalMarty = Instantiate(Orbital);
                        OrbitalMarty.transform.localScale /= 2;
                    }
                    break;
                case (int)ITEMLIST.ORBITAL2:
                    if (GameObject.FindGameObjectsWithTag("OrbitalShoot").Length < 1)
                    {
                        GameObject OrbitalMarty = Instantiate(Orbital2);
                        OrbitalMarty.transform.localScale /= 2;
                        orbital2Timer = 0;
                    }
                    orbital2Instances++;
                    break;
                case (int)ITEMLIST.SPLIT:
                    splitInstances++;
                    break;
                case (int)ITEMLIST.CONTACT:
                    contactInstances++;
                    break;
            }
        }

        switch (weaponHeld)
        {
            case (int)WEAPONS.PISTOL:
                break; //nothing, pistol is default
            case (int)WEAPONS.SHOTGUN:
                fireTimerLength *= 2;
                noExtraShots += 4;
                finalDamageMult -= 0.1f;
                shotAngleCoeff = 0.7f;
                break;
            case (int)WEAPONS.GRENADELAUNCHER:
                fireTimerLength *= 2;
                explodeInstances += 2;
                break;
            case (int)WEAPONS.SWORD:
                normieBullets = false;
                finalDamageMult += 0.75f;
                if (GameObject.FindGameObjectsWithTag("Shield").Length == 0)
                {
                    //Instantiate(Shield);
                }
                break;
        }

        converterDamageMult += ((0.03f) * (1 - HP / maxHP))*converterInstances;
        //Debug.Log(converterDamageMult.ToString());
        trueDamageValue = (damageStat) * (damageMult + converterDamageMult) * finalDamageMult;
    }

    // Update is called once per frame
    void Update()
    {
        // input
        if (isDodging == 0)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
        }

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
                switch (normieBullets)
                {
                    case true:
                        for (int i = -1; i < noExtraShots; i++)
                        {
                            GameObject newObject = Instantiate(PlayerBullet, transform.position, transform.rotation) as GameObject;
                            newObject.transform.localScale = new Vector3(trueDamageValue * 0.0015f + .45f, trueDamageValue * 0.0015f + .45f, trueDamageValue * 0.0015f + .45f);
                            bulletRB = newObject.GetComponent<Rigidbody2D>();
                            currentAngle = 0.3f * shotAngleCoeff * (0.5f * noExtraShots - i - 1);
                            newShotVector = new Vector2(vectorToMouse.x * Mathf.Cos(currentAngle) - vectorToMouse.y * Mathf.Sin(currentAngle), vectorToMouse.x * Mathf.Sin(currentAngle) + vectorToMouse.y * Mathf.Cos(currentAngle));
                            bulletRB.velocity = new Vector2(newShotVector.x * shotSpeed, newShotVector.y * shotSpeed);
                        }
                        break;
                    case false:
                        swordTimer = 0;
                        break;
                }

                if (orbital2Instances > 0)
                {
                    mousePos.x = mousePos3.x;
                    mousePos.y = mousePos3.y;
                    playerPos.x = playerPos3.x + 0.8f * Mathf.Sin(0.08f * orbital2Timer);
                    playerPos.y = playerPos3.y + 0.8f * Mathf.Cos(0.08f * orbital2Timer);
                    vectorToMouse = (mousePos - playerPos).normalized;
                    //firing a number of bullets depending on how many extra shots the player has
                    for (int i = -1; i < noExtraShots; i++)
                    {
                        switch (normieBullets)
                        {
                            case true:
                                GameObject newObject = Instantiate(PlayerBullet, transform.position + new Vector3(0.8f * Mathf.Sin(0.08f * orbital2Timer), 0.8f * Mathf.Cos(0.08f * orbital2Timer), 0), transform.rotation) as GameObject;
                                newObject.transform.localScale = new Vector3(trueDamageValue * 0.0015f * 0.25f * orbital2Instances + .45f, trueDamageValue * 0.0015f * 0.25f * orbital2Instances + .45f, trueDamageValue * 0.0015f * 0.25f * orbital2Instances + .45f);
                                newObject.gameObject.tag = "Orbital Bullet";
                                bulletRB = newObject.GetComponent<Rigidbody2D>();
                                currentAngle = 0.3f * shotAngleCoeff * (0.5f * noExtraShots - i - 1);
                                newShotVector = new Vector2(vectorToMouse.x * Mathf.Cos(currentAngle) - vectorToMouse.y * Mathf.Sin(currentAngle), vectorToMouse.x * Mathf.Sin(currentAngle) + vectorToMouse.y * Mathf.Cos(currentAngle));
                                bulletRB.velocity = new Vector2(newShotVector.x * shotSpeed, newShotVector.y * shotSpeed);
                                break;
                            case false:
                                break;
                        }
                    }
                }

                if (martyCounter * (1.5f*martyInstances) > 6)
                {
                    martyCounter = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        switch (normieBullets)
                        {
                            case true:
                                GameObject newObject = Instantiate(PlayerBullet, transform.position, transform.rotation) as GameObject;
                                newObject.transform.localScale = new Vector3(trueDamageValue * 0.0015f + .4f, trueDamageValue * 0.0015f + .4f, trueDamageValue * 0.0015f + .4f);
                                bulletRB = newObject.GetComponent<Rigidbody2D>();
                                currentAngle = (Mathf.PI / 4) * Mathf.Sin(numberOfMarties * Mathf.PI / 2) + i * Mathf.PI / 2;
                                newShotVector = new Vector2(Mathf.Cos(currentAngle) - Mathf.Sin(currentAngle), Mathf.Sin(currentAngle) + Mathf.Cos(currentAngle));
                                bulletRB.velocity = new Vector2(newShotVector.x * shotSpeed, newShotVector.y * shotSpeed);
                                break;
                            case false:
                                break;
                        }
                    }
                    numberOfMarties++;
                }
                Instantiate(PlayerShootAudio);
                fireTimer = fireTimerLength;
            }
        }

        if (dodgeTimer < -30 + 5*betterDodgeInstances)
        {
            if (Input.GetButton("Dodge"))
            {
                Debug.Log("Dodge the Roll");
                dodgeTimer = dodgeTimerLength * (0.7f+0.3f*betterDodgeInstances);
                //iFrames = dodgeTimerLength;
                isDodging = 1;
                Instantiate(dodgeAudio);
            }
        }

        if (isDodging == 1)
        {
            if (dodgeTimer > 0)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                isDodging = 0;
                gameObject.GetComponent<Collider2D>().enabled = true;
                if (dodgeSplosionInstances > 0)
                {
                    GameObject explodyDodge = Instantiate(dodgeSplosion, transform.position, transform.rotation);
                    explodyDodge.transform.localScale *= 2.5f + 2 * dodgeSplosionInstances;
                }
                if (iFrames <= 7 * betterDodgeInstances)
                {
                    iFrames = 7 * betterDodgeInstances;
                }
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

        //Debug.Log("Damage: " + ((damageStat) * (damageMult + converterDamageMult) * finalDamageMult).ToString() + " damageStat: " + damageStat.ToString() + " damageMult: " + damageMult.ToString() + " converterDamageMult: " + converterDamageMult.ToString() + " finalDamageMult: " + finalDamageMult.ToString());
        trueDamageValue = (damageStat) * (damageMult + converterDamageMult) * finalDamageMult; // Dunno why but if I put this calculation in updatestats it gives a damage value of 0 so it's here for now
    }

    void FixedUpdate()
    {
        if (swordTimer < 4)
        {
            for (int i = -1; i < noExtraShots; i++)
            {
                float fuckinshittyangles = 0.3f * shotAngleCoeff * (0.5f * noExtraShots - i - 1) * 180 / Mathf.PI - 40 + 20f * swordTimer + Vector2.Angle(vectorToMouse, new Vector2(1, 0));

                if (vectorToMouse.y <= 0)
                {
                    fuckinshittyangles *= -1;
                }

                currentAngle = fuckinshittyangles;
                GameObject newObject2 = Instantiate(swordHitbox, transform.position, transform.rotation) as GameObject;
                newObject2.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
                newObject2.transform.localScale *= 1.8f;
            }
        }

        swordTimer++;


        // movement
        rb.velocity = (1+(isDodging*1.5f*(1 + 0.2f*betterDodgeInstances))) * new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        orbital2Timer++;

        dodgeTimer--;
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

        Color tmp = sprite.color;
        if (iFrames > 0)
        {
            if (iFrames % 2 == 0)
            {
                tmp.a = 0f;
                sprite.color = tmp;
            }
            else
            {
                tmp.a = 1f;
                sprite.color = tmp;
            }
        }
        else
        {
            tmp.a = 1f;
            sprite.color = tmp;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
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
            //if (XP >= 180* (level - 1) + levelConstantMul * Mathf.Pow(1.5f, 1.1f * (level-1)))
            if (XP >= 50 * (level + 1) + 2.5f * Mathf.Pow(1.8f, 1.1f * (level + 1)))
            {
                level += 1;
                UpdateStats();
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
