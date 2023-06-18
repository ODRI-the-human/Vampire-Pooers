using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Pool;

public class Attack : MonoBehaviour
{
    public float shotSpeed = 5f;
    public float bulletLengthMult = 1;
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

    public Vector3 mouseVector;
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

    public bool holdDownToShoot = true; // Make this false if the player should need to click for every shot (i.e. with the bat)

    public ObjectPool<GameObject> bulletPool;
    public int numBulletsPossible = 16; // This is for object pooling; this should be plenty for most enemies in default circumstances.
    public bool autoFire = true;

    //public InputActionAsset actions;
    //public InputAction shootAction;
    //public InputAction aimAction;
    public bool isFiring = false;
    public bool isHoldingFire = false; // used by familiars.
    public GameObject shotParticles;
    public float chargeTime = 0; // This is for charging weapons, like the bat n shit.
    public bool isChargedAttack = false;
    public GameObject chargeBar;


    public GameObject reticle;
    public Vector3 reticlePos;


    // For melee!
    public GameObject meleeHitObj;
    public float hitboxSpawnDelay;

    void Start()
    {

        if (stopwatchDebuffAmount == 0)
        {
            stopwatchDebuffAmount = 1;
        }

        if (gameObject.tag == "Player")
        {
            reticle = Instantiate(EntityReferencerGuy.Instance.reticle);
            reticle.transform.SetParent(gameObject.transform);
        }

        //if (actions != null)
        //{
        //    actions.FindActionMap("gameplay").Enable();
        //    actions.FindActionMap("gameplay").FindAction("shoot").performed += OnShoot;
        //    actions.FindActionMap("gameplay").FindAction("shoot").canceled += StopShoot;
        //    aimAction = actions.FindActionMap("gameplay").FindAction("aim");
        //}

        bulletPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(Bullet);
        }, gameObject =>
        {
            gameObject.SetActive(true);
            gameObject.SendMessage("DetermineShotRolls");
        }, gameObject =>
        {
            gameObject.SendMessage("EndOfShotRolls");
            gameObject.SetActive(false);
        }, gameObject =>
        {
            Destroy(gameObject);//gameObject.SendMessage("NoLongerReturnToPool");
        }, true, numBulletsPossible, 300);

        timesFired = -1;
        newAttack = 0;
        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyFamiliar")
        {
            Player = GameObject.Find("newPlayer");
            isEnemy = true;

            // Applying debuffs due to stopwatch.
            fireTimerLength /= stopwatchDebuffAmount;
            shotSpeed *= stopwatchDebuffAmount;
            if (gameObject.GetComponent<NewPlayerMovement>() != null)
            {
                gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed *= stopwatchDebuffAmount;
            }
        }
        cameron = GameObject.Find("Main Camera");
        fireTimerLengthMLT = 1;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        //bool wasChangeInState = false;
        //if (isFiring != context.action.triggered)
        //{
        //    isFiring = context.action.triggered;
        //    SendMessage("StartedOrEndedShooting", isFiring); // Used primarily by NewPlayerMovement to slow the player when shooting.
        //}
        //context.action.performed += ctx => Debug.Log("starte");
        context.action.performed += ctx =>
        {
            //Debug.Log("shooting performed");
            isHoldingFire = true;
            isChargedAttack = false; //resets status of charge attack.
            if (holdDownToShoot)
            {
                SendMessage("AttackStatus", true);
                isFiring = true;
            }
            else
            {
                chargeTime = Time.time;
                chargeBar = Instantiate(EntityReferencerGuy.Instance.chargeBar);
                chargeBar.GetComponent<chargeBarAmt>().owner = gameObject;
                chargeBar.transform.SetParent(GameObject.Find("worldSpaceCanvas").transform);
                chargeBar.transform.localScale = new Vector3(1, 1, 1);
                isFiring = false;
            }
        };
        context.action.canceled += ctx =>
        {
            isHoldingFire = false;
            //Debug.Log("shooting ended");
            if (holdDownToShoot && isFiring)
            {
                SendMessage("AttackStatus", false);
                isFiring = false;
            }
            else
            {
                isFiring = true; // this is because non-hold to shoot weapons actually attack on mouse RELEASE rather than click.
                if (Time.time - chargeTime > (2 / fireTimerActualLength))
                {
                    isChargedAttack = true;
                }
                Destroy(chargeBar);
            }
        };
    }

    public void InputAim(InputAction.CallbackContext context)
    {
        if (gameObject.GetComponent<PlayerInput>().currentControlScheme == "keyboard")
        {
            mouseVector = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            reticlePos = new Vector3(mouseVector.x, mouseVector.y, 0) - cameron.transform.position;
            Vector3 vectorToTarget3 = (new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 0) - Camera.main.WorldToScreenPoint(transform.position));
            vectorToTarget = new Vector2(vectorToTarget3.x, vectorToTarget3.y).normalized;
        }
        else
        {
            vectorToTarget = context.ReadValue<Vector2>();
            reticlePos = 5 * new Vector3(vectorToTarget.x, vectorToTarget.y, 0);
        }
    }

    //void TriggerAShot()
    //{
    //    if (isFiring && fireTimer > (50 / fireTimerActualLength))
    //    {
    //        UseWeapon(false);
    //        timesFired++;
    //        fireTimer = 0;
    //        GameObject ordio = Instantiate(PlayerShootAudio);

    //        if (gameObject.tag == "Player")
    //        {
    //            cameron.GetComponent<cameraMovement>().CameraShake(Mathf.RoundToInt(Mathf.Clamp(gameObject.GetComponent<DealDamage>().damageToPresent, 0, 75) / 6));
    //        }
    //    }
    //}
    // Update is called once per frame, as you know


    void CreateShotFX()
    {
        GameObject ordio = Instantiate(PlayerShootAudio, transform.position, transform.rotation);
        GameObject particles = Instantiate(shotParticles, transform.position, transform.rotation);

        if (particles.GetComponent<ParticleSystem>() != null)
        {
            particles.transform.rotation = Quaternion.LookRotation(vectorToTarget, new Vector3(1, 0, 0)) * Quaternion.Euler(-90, 0, 0);
            var ps = particles.GetComponent<ParticleSystem>();
            ps.Emit(3);
        }
    }


    void Update()
    {
        trueDamageValue = gameObject.GetComponent<DealDamage>().finalDamageStat;
        fireTimerLength = Mathf.Clamp(fireTimerLength, 0, 99999);
        fireTimerActualLength = Mathf.Clamp(50 / (fireTimerLength * fireTimerLengthMLT / fireTimerDIV), 0, 50);

        if (reticle != null)
        {
            Vector3 blobob;

            if (gameObject.GetComponent<PlayerInput>().currentControlScheme == "keyboard")
            {
                blobob = reticlePos + cameron.transform.position;
                //reticle.transform.position = blobob; //cameron.transform.position + reticlePos + new Vector3(0, 0, 5);
            }
            else
            {
                blobob = reticlePos + transform.position;
            }

            reticle.transform.position = new Vector3(blobob.x, blobob.y, -8);
            Color retCol = reticle.GetComponent<SpriteRenderer>().color;
            Vector3 retPosZ0 = new Vector3(blobob.x, blobob.y, 0);
            retCol.a = (retPosZ0 - transform.position).magnitude;
            reticle.GetComponent<SpriteRenderer>().color = retCol;
        }

        //if (gameObject.GetComponent<weaponType>() != null) // For capping your fire rate to the proper amount based on what weapon you're using.
        //{
        //    switch (gameObject.GetComponent<weaponType>().weaponHeld)
        //    {
        //        case (int)ITEMLIST.BAT:
        //            fireTimerActualLength = Mathf.Clamp(fireTimerActualLength, 0, 12);
        //            break;
        //    }
        //}

        if (reTargetTimer <= 0)
        {
            ReTarget();
            reTargetTimer = reTargetTimerLength;
        }

        if (currentTarget != null && getEnemyPos)
        {
            vectorToTarget = (currentTarget.transform.position - gameObject.transform.position).normalized;
        }

        int isDodging = 0;
        if (gameObject.GetComponent<NewPlayerMovement>() != null)
        {
            isDodging = gameObject.GetComponent<NewPlayerMovement>().isDodging;
        }

        switch (playerControlled)
        {
            case true:
                if (isFiring && fireTimer > (50 / fireTimerActualLength) && isDodging == 0 && vectorToTarget != Vector2.zero)
                {
                    UseWeapon(false);
                    timesFired++;
                    fireTimer = 0;
                    CreateShotFX();

                    if (gameObject.tag == "Player")
                    {
                        cameron.GetComponent<cameraMovement>().CameraShake(Mathf.RoundToInt(Mathf.Clamp(gameObject.GetComponent<DealDamage>().damageToPresent, 0, 75) / 6));
                    }
                }
                break;
            case false:
                if (fireTimer > (50 / fireTimerActualLength) && doShootAutomatically && isDodging == 0 && autoFire)
                {
                    if (currentTarget == null)
                    {
                        ReTarget();
                    }

                    if ((currentTarget.transform.position - gameObject.transform.position).magnitude < visionRange)
                    {
                        isHoldingFire = true;
                    }
                    else
                    {
                        isHoldingFire = false;
                    }

                    if (currentTarget != null && isHoldingFire && specialFireType != 2)
                    {
                        UseWeapon(false);
                        timesFired++;
                        fireTimer = 0;
                        //CreateShotFX();
                    }
                }
                break;
        }

        if (!holdDownToShoot)
        {
            isFiring = false; // essentially just makes it so isfiring just gets disabled by default if they need to hold down.
        }
    }

    public void AttackStatus(bool didStart)
    {
        //shut thje fuck up
    }

    public void OnShootEffects()
    {
        //shut up
    }

    public void UseWeapon(bool angleOverride) // angleOverride is false most of the time, but true if you want to use an input currentAngle.
    {
        SendMessage("OnShootEffects");

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
                case 3: // For melee.
                    if (!angleOverride)
                    {
                        //currentAngle = shotAngleCoeff * (Mathf.PI / 4) * (-0.5f * (noExtraShots) + 0.5f * i);
                        currentAngle = 3 * ((-0.25f * noExtraShots) + (i + 1) * 0.5f);
                    }
                    StartCoroutine(SpawnMelee(currentAngle, new Vector3(0, 0, 0), null, 1, 1, 1, true));
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
                //case 6: //enptic basball bat
                //    GameObject battery = gameObject.GetComponent<weaponType>().spawnedBat;
                //    battery.GetComponent<faceInFunnyDirection>().Attackment(50 / fireTimerActualLength);
                //    break;
            }
        }
    }

    public void SpawnAttack(float currentAngle)
    {
        GameObject newObject = bulletPool.Get();//Instantiate(Bullet, transform.position, transform.rotation);
        newObject.transform.position = transform.position;
        newObject.transform.localScale = new Vector3(trueDamageValue * 0.0015f + .45f * scaleAddMult, trueDamageValue * 0.0015f + .45f * scaleAddMult, trueDamageValue * 0.0015f + .45f * scaleAddMult);
        bulletRB = newObject.GetComponent<Rigidbody2D>();
        if (!doAim)
        {
            vectorToTarget = new Vector2(1,0);
        }
        newShotVector = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle)).normalized;
        velToGiveBullets = newShotVector * shotSpeed;
        bulletRB.velocity = velToGiveBullets;
        newObject.GetComponent<DealDamage>().master = gameObject.GetComponent<DealDamage>().master;
        if (attachItems)
        {
            newObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            newObject.GetComponent<Rigidbody2D>().simulated = true;
            //newObject.AddComponent<KillBullets>();
            newObject.GetComponent<weaponType>().weaponHeld = newObject.GetComponent<weaponType>().weaponHeld;
            newObject.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
            newObject.GetComponent<DealDamage>().finalDamageMult = gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().damageAdd += Crongus + levelDamageBonus; // applies converter damage bonus to bullets
        }
        else
        {
            newObject.GetComponent<DealDamage>().finalDamageMult = gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().procCoeff = gameObject.GetComponent<DealDamage>().procCoeff;
            newObject.GetComponent<DealDamage>().damageBase = gameObject.GetComponent<DealDamage>().damageBase;
            newObject.GetComponent<DealDamage>().damageMult = gameObject.GetComponent<DealDamage>().damageMult;
            newObject.GetComponent<DealDamage>().finalDamageMult = gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().massCoeff = massToGiveBullets;
            newObject.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
            newObject.GetComponent<DealDamage>().finalDamageDIV = gameObject.GetComponent<DealDamage>().finalDamageDIV;
            newObject.GetComponent<weaponType>().weaponHeld = newObject.GetComponent<weaponType>().weaponHeld;
            newObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
        }

        //switch (specialFireType)
        //{
        //    case 4:
        //        newObject.AddComponent<ItemCREEPSHOT>();
        //        break;
        //}
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

    public IEnumerator SpawnMelee(float thisAngle, Vector3 pos, GameObject objToIgnore, float damageMult, float delayMult, float scaleMult, bool allowSplit) // pos is for moving the hitbox elsewhere, while objToIgnore is to have the hitboxes ignore a certain object, mainly for split shots.
    {
        bool isThisAttackCharged = isChargedAttack;
        GameObject hitBox = Instantiate(meleeHitObj, transform.position + pos, transform.rotation);
        hitBox.SetActive(false);

        yield return new WaitForSeconds(0 * delayMult);

        hitBox.SetActive(true);
        hitBox.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
        hitBox.transform.rotation = Quaternion.LookRotation(vectorToTarget, new Vector3(1, 0, 0)) * Quaternion.Euler(0, 90, 180 + thisAngle * 360 / (2 * Mathf.PI));
        hitBox.transform.localScale *= scaleMult;
        hitBox.GetComponent<DealDamage>().owner = gameObject;
        hitBox.GetComponent<DealDamage>().finalDamageMult = damageMult * gameObject.GetComponent<DealDamage>().finalDamageMult;
        Debug.Log("Attack damage mult: " + (damageMult * gameObject.GetComponent<DealDamage>().finalDamageMult).ToString());
        hitBox.GetComponent<meleeGeneral>().isCharged = isThisAttackCharged;

        if (objToIgnore != null)
        {
            Physics2D.IgnoreCollision(hitBox.GetComponent<Collider2D>(), objToIgnore.GetComponent<Collider2D>(), true);
        }

        if (!allowSplit)
        {
            Debug.Log("no splite1");
            hitBox.AddComponent<ItemSPLIT>();
            hitBox.GetComponent<ItemSPLIT>().canSplit = false;
        }


        //vectorMan = vectorToTarget;
        ////vectorMan = Camera.main.ScreenToWorldPoint(mouseVector) - transform.position;

        //if (vectorMan.y > 0 && vectorMan.x > 0)
        //{
        //    fuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
        //}
        //else if (vectorMan.y > 0 && vectorMan.x < 0)
        //{
        //    fuckAngle = 180 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x);
        //}
        //else if (vectorMan.y < 0 && vectorMan.x < 0)
        //{
        //    fuckAngle = (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 180;
        //}
        //else if (vectorMan.y < 0 && vectorMan.x > 0)
        //{
        //    fuckAngle = 90 + (180 / Mathf.PI) * Mathf.Atan(vectorMan.y / vectorMan.x) + 270;
        //}

        //fuckAngle += currentAngle * 180/Mathf.PI;

        //GameObject Swordo = Instantiate(darkArtSword, transform.position, Quaternion.Euler(0,0,fuckAngle));
        //Swordo.GetComponent<darkArtMovement>().initAngle = fuckAngle;
        //Swordo.GetComponent<darkArtMovement>().LorR = newAttack;
        //Swordo.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
        //Swordo.GetComponent<DealDamage>().owner = gameObject;

        //if (gameObject.tag == "Player" || gameObject.tag == "PlayerBullet")
        //{
        //    Swordo.tag = "PlayerBullet";
        //}
        //else if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
        //{
        //    Swordo.tag = "enemyBullet";
        //}

    }

    void FixedUpdate()
    {
        fireTimer++;
        reTargetTimer--;
    }

    public void itemsAdded(bool nothing)
    {
        if (gameObject.tag == "Player")
        {
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
            foreach (GameObject bullet in bullets)
            {
                if (bullet.GetComponent<Bullet_Movement>() != null)
                {
                    Destroy(bullet);
                }
            }
            if (bulletPool != null)
            {
                bulletPool.Clear();

            }
            //Debug.Log("funker dunker");
            timesFired = -1;
        }
    }
}
