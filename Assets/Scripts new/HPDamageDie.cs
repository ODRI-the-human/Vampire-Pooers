using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDamageDie : MonoBehaviour
{

    int HP;
    public GameObject Barry63;
    public GameObject PlayerDieAudio;
    public GameObject PlayerHurtAudio;
    public Rigidbody2D rb;
    float iFramesTimer = 50;
    float iFrames = 0;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
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
    }

    void FixedUpdate()
    {
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

        iFrames -= 1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (iFrames < 0)
        {
            HP -= col.gameObject.GetComponent<DealDamage>().finalDamageStat;
            Debug.Log("Collisisisinson");
            Instantiate(PlayerHurtAudio);
            iFrames = iFramesTimer;
        }
    }
}
