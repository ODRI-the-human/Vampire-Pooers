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
    bool playerControlled;
    public int specialFireType;

    public int timesFired;
    public int newAttack; // alternates between 0 and 1 when the player fires. Used for certain items.

    void Start()
    {
        newAttack = 0;
        if (gameObject.tag == "Hostile")
        {
            Player = GameObject.Find("newPlayer");
            playerControlled = false;
            if (Player.GetComponent<ItemSTOPWATCH>() != null)
            {
                fireTimerLength *= 1 + (0.25f) * Player.GetComponent<ItemSTOPWATCH>().instances;
                fireTimerLength /= 1 + (0.25f) * Player.GetComponent<ItemSTOPWATCH>().instances;
            }
        }
        else
        {
            playerControlled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        trueDamageValue = gameObject.GetComponent<DealDamage>().finalDamageStat;
        if (fireTimer + fireTimerLength * (fireTimerLengthMLT - 1) < 0)
        {
            switch (playerControlled)
            {
                case true:
                    vectorToTarget = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y).normalized;
                    if (Input.GetButton("Fire1"))
                    {
                        UseWeapon();
                        fireTimer = fireTimerLength;
                    }
                    break;
                case false:
                    vectorToTarget = (Player.transform.position - gameObject.transform.position).normalized;
                    UseWeapon();
                    fireTimer = fireTimerLength;
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
                    Instantiate(PlayerShootAudio);
                    break;
                case 1:
                    currentAngle = (Mathf.PI / 4) * i;
                    SpawnAttack(currentAngle);
                    Instantiate(PlayerShootAudio);
                    break;
                case 2:
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
    }

    void FixedUpdate()
    {
        fireTimer -= 1;
    }
}
