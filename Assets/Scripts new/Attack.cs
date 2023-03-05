using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float shotSpeed = 5f;
    Vector2 newShotVector;
    Rigidbody2D bulletRB;
    public float currentAngle;
    public Vector2 vectorToTarget;
    public int noExtraShots = 0;
    public float shotAngleCoeff = 1;
    public float trueDamageValue;
    public float fireTimerLengthMLT = 1;
    public GameObject Bullet;
    public float fireTimerLength = 25;
    public float fireTimer = 25;
    public GameObject PlayerShootAudio;
    public GameObject Player;
    public bool playerControlled = false;
    public int specialFireType;
    public GameObject darkArtSword;
    public float fireTimerDIV = 1;
    public bool attachItems = true;

    public bool doAim = true; // this is for things like the 8-direction shooty enemy (should be false for them), just makes it so the enemy does or doesn't change its shot angle depending on where the target is.
    public float fireTimerActualLength;

    public int visionRange = 8;

    public int reTargetTimerLength = 100;
    public int reTargetTimer = 0;
    public GameObject currentTarget;

    public int timesFired = 0;
    public int newAttack; // alternates between 0 and 1 when the player fires. Used for certain items.

    public float Crongus = 0; // records total converter damage bonus.

    public float levelDamageBonus = 0;
    public float scaleAddMult = 1;

    Vector3 mouseVector;
    Vector3 vectorMan;
    float fuckAngle;

    GameObject cameron;

    public float stopwatchDebuffAmount;

    public bool doShootAutomatically = true;
    bool isEnemy = false;
    public bool getEnemyPos = true;
    public float angleAddAmount = 0;

    public Vector3 velToGiveBullets;
    public float massToGiveBullets = 0.5f; // This is for familiars and stuff so I can set their bullets mass coeff manually.

    void Start()
    {
        timesFired = -1;
        newAttack = 0;
        stopwatchDebuffAmount = 1;
        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyFamiliar")
        {
            Player = GameObject.Find("newPlayer");
            isEnemy = true;
            if (Player.GetComponent<ItemSTOPWATCH>() != null)
            {
                stopwatchDebuffAmount = 1 / (0.4f * Player.GetComponent<ItemSTOPWATCH>().instances + 1);
                fireTimerLength /= stopwatchDebuffAmount;
                shotSpeed *= stopwatchDebuffAmount;
                gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed *= stopwatchDebuffAmount;
            }
        }
        cameron = GameObject.Find("Main Camera");
    }

    // Update is called once per frame, as you know
    void Update()
    {
        trueDamageValue = gameObject.GetComponent<DealDamage>().finalDamageStat;
        fireTimerLength = Mathf.Clamp(fireTimerLength, 0, 99999);
        fireTimerActualLength = Mathf.Clamp(50 / (fireTimerLength * fireTimerLengthMLT / fireTimerDIV),0,25);

        if (reTargetTimer <= 0)
        {
            ReTarget();
            reTargetTimer = reTargetTimerLength;
        }

        if (currentTarget != null && getEnemyPos)
        {
            vectorToTarget = (currentTarget.transform.position - gameObject.transform.position).normalized;
        }

        if (fireTimer > Mathf.Clamp(fireTimerLength * fireTimerLengthMLT / fireTimerDIV, 2, 9999999999) && doShootAutomatically)
        {
            switch (playerControlled)
            {
                case true:
                    vectorToTarget = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y).normalized;
                    if (Input.GetButton("Fire1"))
                    {
                        UseWeapon(false);
                        timesFired++;
                        fireTimer = 0;
                        GameObject ordio = Instantiate(PlayerShootAudio);
                        ordio.GetComponent<AudioSource>().pitch *= Random.Range(0.8f, 1.2f);
                    }
                    break;
                case false:
                    if (currentTarget == null)
                    {
                        ReTarget();
                    }
                    if ((currentTarget.transform.position - gameObject.transform.position).magnitude < visionRange && specialFireType != 2)
                    {
                        UseWeapon(false);
                        timesFired++;
                        fireTimer = 0;
                        GameObject ordio = Instantiate(PlayerShootAudio);
                        ordio.GetComponent<AudioSource>().pitch *= Random.Range(0.8f, 1.2f);
                    }
                    break;
            }
        }
    }

    public void UseWeapon(bool angleOverride) // angleOverride is false most of the time, but true if you want to use an input currentAngle.
    {
        switch (newAttack)
        {
            case 0:
                newAttack = 1;
                break;
            case 1:
                newAttack = 0;
                break;
        }

        for (int i = -1; i < noExtraShots; i++)
        {
            switch (specialFireType)
            {
                case 0:
                    if (!angleOverride)
                    {
                        currentAngle = 0.3f * shotAngleCoeff * (0.5f * noExtraShots - i - 1);
                    }
                    SpawnAttack(currentAngle);
                    break;
                case 1:
                    if (!angleOverride)
                    {
                        currentAngle = shotAngleCoeff * (Mathf.PI / 4) * i + angleAddAmount;
                    }
                    SpawnAttack(currentAngle);
                    break;
                case 2:
                    break; // For enemies that don't shoot.
                case 3: // For berserk.
                    if (!angleOverride)
                    {
                        currentAngle = (Mathf.PI / 4) * (i + 1);
                    }
                    SpawnDarkart();
                    break;
                case 4: // For Monstro enemy.
                    for (int j = 0; j < 15; j++)
                    {
                        currentAngle = Random.Range(-0.5f, 0.5f) + 0.3f * shotAngleCoeff * (0.5f * noExtraShots - i - 1);
                        float normieShotSpeed = shotSpeed;
                        shotSpeed *= Random.Range(0.8f, 1.8f);
                        SpawnAttack(currentAngle);
                        shotSpeed = normieShotSpeed;
                    }
                    break;
                case 5:
                    Vector3 neq;

                    if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
                    {
                        neq = currentTarget.transform.position;
                    }
                    else
                    {
                        neq = vectorToTarget;
                    }

                    if (!angleOverride)
                    {
                        currentAngle = 0.3f * shotAngleCoeff * (0.5f * noExtraShots - i - 1);
                    }
                    StartCoroutine(gameObject.GetComponent<lightningFireV2>().Target(neq, currentAngle, noExtraShots));
                    break;
            }
        }
    }

    public void SpawnAttack(float currentAngle)
    {
        GameObject newObject = Instantiate(Bullet, transform.position, transform.rotation);
        newObject.transform.localScale = new Vector3(trueDamageValue * 0.0015f + .45f * scaleAddMult, trueDamageValue * 0.0015f + .45f * scaleAddMult, trueDamageValue * 0.0015f + .45f * scaleAddMult);
        bulletRB = newObject.GetComponent<Rigidbody2D>();
        if (!doAim)
        {
            vectorToTarget = new Vector2(1,0);
        }
        newShotVector = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle));
        velToGiveBullets = newShotVector * shotSpeed;
        bulletRB.velocity = velToGiveBullets;
        newObject.GetComponent<DealDamage>().master = gameObject.GetComponent<DealDamage>().master;
        if (attachItems)
        {
            //newObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            newObject.GetComponent<ItemHolder>().doTheShit = false;
            newObject.GetComponent<Rigidbody2D>().simulated = true;
            //newObject.AddComponent<KillBullets>();
            newObject.GetComponent<weaponType>().weaponHeld = newObject.GetComponent<weaponType>().weaponHeld;
            newObject.GetComponent<DealDamage>().owner = gameObject;
            //newObject.GetComponent<DealDamage>().isBulletClone = true;
            newObject.GetComponent<DealDamage>().finalDamageMult *= gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().damageBase += Crongus + levelDamageBonus; // applies converter damage bonus to bullets
            if (gameObject.tag == "Player")
            {
                cameron.GetComponent<cameraMovement>().CameraShake(Mathf.RoundToInt(gameObject.GetComponent<DealDamage>().damageToPresent / 6));
            }
        }
        else
        {
            newObject.GetComponent<DealDamage>().finalDamageMult = gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().procCoeff = gameObject.GetComponent<DealDamage>().procCoeff;
            newObject.GetComponent<DealDamage>().damageBase = gameObject.GetComponent<DealDamage>().damageBase;
            newObject.GetComponent<DealDamage>().damageMult = gameObject.GetComponent<DealDamage>().damageMult;
            newObject.GetComponent<DealDamage>().finalDamageMult = gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().massCoeff = massToGiveBullets;
            newObject.GetComponent<DealDamage>().owner = gameObject;
            newObject.GetComponent<DealDamage>().finalDamageDIV = gameObject.GetComponent<DealDamage>().finalDamageDIV;
            newObject.GetComponent<weaponType>().weaponHeld = newObject.GetComponent<weaponType>().weaponHeld;
            newObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
        }

        switch (specialFireType)
        {
            case 4:
                newObject.AddComponent<ItemCREEPSHOT>();
                break;
        }
    }

    void ReTarget()
    {
        GameObject[] gos;
        if (gameObject.tag == "Player" || gameObject.tag == "Familiar")
        {
            gos = GameObject.FindGameObjectsWithTag("Hostile");
        }
        else
        {
            gos = GameObject.FindGameObjectsWithTag("Player");
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        currentTarget = closest;
    }

    void SpawnDarkart()
    {
        mouseVector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        vectorMan = Camera.main.ScreenToWorldPoint(mouseVector) - transform.position;
        cameron.GetComponent<cameraMovement>().CameraShake(25);

        if (vectorMan.y > 0 && vectorMan.x > 0)
        {
            fuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
        }
        else if (vectorMan.y > 0 && vectorMan.x < 0)
        {
            fuckAngle = 180 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
        }
        else if (vectorMan.y < 0 && vectorMan.x < 0)
        {
            fuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 180;
        }
        else if (vectorMan.y < 0 && vectorMan.x > 0)
        {
            fuckAngle = 90 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 270;
        }

        fuckAngle += currentAngle * 180/Mathf.PI;

        GameObject Swordo = Instantiate(darkArtSword, transform.position, Quaternion.Euler(0,0,fuckAngle));
        Swordo.GetComponent<darkArtMovement>().initAngle = fuckAngle;
        Swordo.GetComponent<darkArtMovement>().LorR = newAttack;
        Swordo.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
        Swordo.GetComponent<DealDamage>().owner = gameObject;
        
        if (gameObject.tag == "Player" || gameObject.tag == "PlayerBullet")
        {
            Swordo.tag = "PlayerBullet";
        }
        else if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
        {
            Swordo.tag = "enemyBullet";
        }

    }

    void FixedUpdate()
    {
        fireTimer++;
        reTargetTimer--;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            timesFired = -1;
        }
    }
}
