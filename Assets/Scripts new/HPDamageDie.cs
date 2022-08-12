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
    float iFramesTimer = 50;
    float iFrames = 0;
    public SpriteRenderer sprite;
    bool playerControlled;
    int colorChangeTimer = 0;
    Color originalColor;
    GameObject Player;

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
            //Debug.Log("Owned Lu zer");
            Instantiate(Barry63, new Vector3(0, 0, -1), new Quaternion(1, Mathf.PI, 0, 0));
            Instantiate(PlayerDieAudio);
        }

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }

    void FixedUpdate()
    {
        //Color tmp = sprite.color;
        //if (iFrames > 0)
        //{
        //    if (iFrames % 2 == 0)
        //    {
        //        //tmp.a = 0f;
        //        //sprite.color = tmp;
        //    }
        //    else
        //    {
        //        tmp.a = 1f;
        //        sprite.color = tmp;
        //    }
        //}
        //else
        //{
        //    tmp.a = originalColor.a;
        //    sprite.color = tmp;
        //}

        if (colorChangeTimer == 0)
        {
            sprite.color = originalColor;
        }

        iFrames--;
        colorChangeTimer--;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != gameObject.tag)
        {
            if (playerControlled == false)
            {
                Debug.Log("Dogass");
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
