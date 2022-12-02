using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float shotSpeed = 5f;
    Vector2 newShotVector;
    Rigidbody2D bulletRB;
    public float currentAngle;
    Vector2 vectorToTarget;
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

    public int timesFired = -1;
    public int newAttack; // alternates between 0 and 1 when the player fires. Used for certain items.

    public float Crongus = 0; // records total converter damage bonus.

    public float levelDamageBonus = 0;
    public float scaleAddMult = 1;

    Vector3 mouseVector;
    Vector3 vectorMan;
    float fuckAngle;

    GameObject cameron;

    void Start()
    {
        newAttack = 0;
        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet" || gameObject.tag == "enemyFamiliar")
        {
            Player = GameObject.Find("newPlayer");
            if (Player.GetComponent<ItemSTOPWATCH>() != null)
            {
                fireTimerLength *= 1 + (0.25f) * Player.GetComponent<ItemSTOPWATCH>().instances;
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

        if (fireTimer > Mathf.Clamp(fireTimerLength * fireTimerLengthMLT / fireTimerDIV, 2, 9999999999))
        {
            switch (playerControlled)
            {
                case true:
                    vectorToTarget = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y).normalized;
                    if (Input.GetButton("Fire1"))
                    {
                        UseWeapon();
                        fireTimer = 0;
                        Instantiate(PlayerShootAudio);
                    }
                    break;
                case false:
                    if (currentTarget == null)
                    {
                        ReTarget();
                    }
                    if ((currentTarget.transform.position - gameObject.transform.position).magnitude < visionRange)
                    {
                        vectorToTarget = (currentTarget.transform.position - gameObject.transform.position).normalized;
                        UseWeapon();
                        fireTimer = 0;
                        Instantiate(PlayerShootAudio);
                    }
                    break;
            }
        }
    }

    void UseWeapon()
    {
        timesFired++;

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
                    currentAngle = 0.3f * shotAngleCoeff * (0.5f * noExtraShots - i - 1);
                    SpawnAttack(currentAngle);
                    break;
                case 1:
                    currentAngle = (Mathf.PI / 4) * i;
                    SpawnAttack(currentAngle);
                    break;
                case 2:
                    break; // For enemies that don't shoot.
                case 3:
                    currentAngle = (Mathf.PI / 4) * (i+1);
                    SpawnDarkart();
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
        bulletRB.velocity = newShotVector * shotSpeed;
        if (attachItems)
        {
            //newObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            newObject.GetComponent<ItemHolder>().doTheShit = false;
            newObject.GetComponent<Rigidbody2D>().simulated = true;
            //newObject.AddComponent<KillBullets>();
            newObject.GetComponent<weaponType>().weaponHeld = newObject.GetComponent<weaponType>().weaponHeld;
            newObject.GetComponent<DealDamage>().owner = gameObject;
            newObject.GetComponent<DealDamage>().finalDamageMult *= gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().damageBase += Crongus + levelDamageBonus; // applies converter damage bonus to bullets
        }
        else
        {
            newObject.GetComponent<DealDamage>().finalDamageMult = gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().procCoeff = gameObject.GetComponent<DealDamage>().procCoeff;
            newObject.GetComponent<DealDamage>().damageBase = gameObject.GetComponent<DealDamage>().damageBase;
            newObject.GetComponent<DealDamage>().damageMult = gameObject.GetComponent<DealDamage>().damageMult;
            newObject.GetComponent<DealDamage>().finalDamageMult = gameObject.GetComponent<DealDamage>().finalDamageMult;
            newObject.GetComponent<DealDamage>().knockBackCoeff = gameObject.GetComponent<DealDamage>().knockBackCoeff;
            newObject.GetComponent<DealDamage>().finalDamageDIV = gameObject.GetComponent<DealDamage>().finalDamageDIV;
            newObject.GetComponent<weaponType>().weaponHeld = newObject.GetComponent<weaponType>().weaponHeld;
            newObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
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
        cameron.GetComponent<cameraMovement>().CameraShake();

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
}
