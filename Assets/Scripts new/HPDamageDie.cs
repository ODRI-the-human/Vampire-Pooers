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

    void Update()
    {
        if (HP <= 0)
        {
            Instantiate(XP, transform.position, transform.rotation);
            Instantiate(PlayerDieAudio);
            Destroy(gameObject);
            EventManager.DeathEffects(transform.position);
            //poisonSplosm = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().poisonSplosm;
            //Instantiate(poisonSplosm, transform.position, transform.rotation);
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
    }

    void OnCollisionEnter2D(Collision2D col)
    {
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
                if (iFrames < 0)
                {
                    HP -= col.gameObject.GetComponent<DealDamage>().finalDamageStat;
                    Instantiate(PlayerHurtAudio);
                    if (playerControlled == true)
                    {
                        iFrames = iFramesTimer;
                    }
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col) // just creep/orbitals lmao
    {
        if ((col.gameObject.tag == "enemyBullet" && gameObject.tag == "Player") || (col.gameObject.tag == "PlayerBullet" && gameObject.tag == "Hostile")) // otherwise xp drops would probably deal damage
        {
            if (iFrames < 0)
            {
                HP -= col.gameObject.GetComponent<DealDamage>().finalDamageStat;
                iFrames = 4;
                if (playerControlled == true)
                {
                    Instantiate(PlayerHurtAudio);
                }
                if (playerControlled == false)
                {
                    sprite.color = Color.red;
                    colorChangeTimer = 1;
                }
            }
        }
    }
}
