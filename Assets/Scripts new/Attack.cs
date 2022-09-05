using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float shotSpeed = 5f;
    Vector2 newShotVector;
    Rigidbody2D bulletRB;
    float currentAngle;
    Vector2 vectorToTarget;
    public int noExtraShots = 0;
    public float shotAngleCoeff = 1;
    public float trueDamageValue;
    [HideInInspector] public float fireTimerLengthMLT = 1;
    public GameObject Bullet;
    public float fireTimerLength = 25;
    public float fireTimer = 0f;
    public GameObject PlayerShootAudio;
    GameObject Player;
    public bool playerControlled = false;
    public int specialFireType;
    public GameObject darkArtSword;
    public float fireTimerDIV = 1;

    public int timesFired;
    public int newAttack; // alternates between 0 and 1 when the player fires. Used for certain items.

    Vector3 mouseVector;
    Vector3 vectorMan;
    float fuckAngle;

    void Start()
    {
        newAttack = 0;
        if (gameObject.tag == "Hostile" || gameObject.tag == "enemyBullet")
        {
            Player = GameObject.Find("newPlayer");
            if (Player.GetComponent<ItemSTOPWATCH>() != null)
            {
                fireTimerLength *= 1 + (0.25f) * Player.GetComponent<ItemSTOPWATCH>().instances;
                fireTimerLength /= 1 + (0.25f) * Player.GetComponent<ItemSTOPWATCH>().instances;
            }
        }
    }

    // Update is called once per frame, as you know
    void Update()
    {
        trueDamageValue = gameObject.GetComponent<DealDamage>().finalDamageStat;

        if (gameObject.tag == "Player")
        {
            Debug.Log((fireTimerLength * (1 - fireTimerDIV) / fireTimerDIV).ToString());
        }

        if (fireTimer + fireTimerLength*(1-fireTimerDIV)/fireTimerDIV + fireTimerLength * (fireTimerLengthMLT - 1) < 0)
        {
            switch (playerControlled)
            {
                case true:
                    vectorToTarget = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y).normalized;
                    if (Input.GetButton("Fire1"))
                    {
                        UseWeapon();
                        fireTimer = fireTimerLength;
                        Instantiate(PlayerShootAudio);
                    }
                    break;
                case false:
                    vectorToTarget = (Player.transform.position - gameObject.transform.position).normalized;
                    UseWeapon();
                    fireTimer = fireTimerLength;
                    Instantiate(PlayerShootAudio);
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
        newObject.transform.localScale = new Vector3(trueDamageValue * 0.0015f + .45f, trueDamageValue * 0.0015f + .45f, trueDamageValue * 0.0015f + .45f);
        bulletRB = newObject.GetComponent<Rigidbody2D>();
        newShotVector = new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle));
        bulletRB.velocity = newShotVector * shotSpeed;
        newObject.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
        newObject.GetComponent<weaponType>().weaponHeld = newObject.GetComponent<weaponType>().weaponHeld;
        newObject.GetComponent<DealDamage>().owner = gameObject;
    }

    void SpawnDarkart()
    {
        mouseVector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        vectorMan = Camera.main.ScreenToWorldPoint(mouseVector) - transform.position;

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
        fireTimer -= 1;
    }
}
