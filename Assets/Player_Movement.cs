using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Movement : MonoBehaviour
{

    public float moveSpeed = 5f;
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
    int HP;
    int noExtraShots = 0;
    public float damageStatInitial = 1f;
    public float damageStat;
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

    private Vector2 moveDirection;
    public List<int> itemsHeld = new List<int>();

    void Start()
    {
        UpdateStats();
        HP = maxHP;
        SetStatsText();
    }

    void SetStatsText()
    {
        HPText.text = "HP: " + HP.ToString() + "/" + maxHP.ToString();
        XPText.text = "XP: " + XP.ToString();
        DMGText.text = "DMG: " + (50 * damageStat).ToString();
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
        damageStat = damageStatInitial;
        fireTimerLength = initialFireTimerLength;
        noExtraShots = 0;

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
                    damageStat += 0.5f;
                    break;
                case (int)ITEMLIST.DMGMLT2:
                    damageStat *= 2;
                    break;
                case (int)ITEMLIST.FIRERATE:
                    fireTimerLength /= 1.15f;
                    break;
                case (int)ITEMLIST.SOY:
                    fireTimerLength /= 5;
                    damageStat /= 3;
                    break;
                case (int)ITEMLIST.MORESHOT:
                    noExtraShots += 1;
                    break;
            }
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
                for (int i = -1; i < noExtraShots; i++)
                {
                    GameObject newObject = Instantiate(PlayerBullet, transform.position, transform.rotation) as GameObject;
                    newObject.transform.localScale = new Vector3(0.2f * damageStat + 0.2f, 0.2f * damageStat + 0.2f, 0.2f * damageStat + 0.2f);
                    bulletRB = newObject.GetComponent<Rigidbody2D>();
                    currentAngle = 0.3f * (0.5f * noExtraShots - i - 1);
                    newShotVector = new Vector2(vectorToMouse.x * Mathf.Cos(currentAngle) - vectorToMouse.y * Mathf.Sin(currentAngle), vectorToMouse.x * Mathf.Sin(currentAngle) + vectorToMouse.y * Mathf.Cos(currentAngle));
                    bulletRB.velocity = new Vector2(newShotVector.x * shotSpeed, newShotVector.y * shotSpeed);
                }
                Instantiate(PlayerShootAudio);
                fireTimer = fireTimerLength;
            }
        }

        // >dies
        if (HP <= 0)
        {
            Destroy(Player);
            Debug.Log("Owned Lu zer");
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
    }

    void FixedUpdate()
    {
        // movement
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        // reducing cooldown timer of weapon
        fireTimer -= 1;

        // reducing iframes
        iFrames -= 1;
    }

    // if player has skill issue and consequently gets hit (bad at game)
    void OnCollisionEnter2D(Collision2D col)
    {
        // if player has skill issue and consequently gets hit (bad at game)
        if (col.gameObject.tag == "Hostile")
        {
            if (iFrames < 0)
            {
                HP -= 50;
                //Debug.Log("Collided");
                iFrames = iFramesTimer;
                Instantiate(PlayerHurtAudio);
                SetStatsText();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
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
    }
}
