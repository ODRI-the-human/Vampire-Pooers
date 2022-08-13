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
    bool playerControlled;
    int colorChangeTimer = 0;
    Color originalColor;
    GameObject Player;
    public GameObject XP;

    // Start is called before the first frame update
    void Awake()
    {
        originalColor = sprite.color;
        MaxHP = 100;
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

            Destroy(gameObject);
            Instantiate(Barry63, new Vector3(0, 0, -1), new Quaternion(1, Mathf.PI, 0, 0));
            Instantiate(PlayerDieAudio);
            Instantiate(XP, transform.position, transform.rotation);

            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
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
}
