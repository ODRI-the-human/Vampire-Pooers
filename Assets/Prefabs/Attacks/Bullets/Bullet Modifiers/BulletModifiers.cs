using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletModifiers : MonoBehaviour
{
    public string[] effectNames;
    public List<int> bulletEffects = new List<int>();
    int timer = 0;

    void Start()
    {
        foreach (string effectName in effectNames)
        {
            bulletEffects.Add((int)Enum.Parse(typeof(BULLETEFFECTS), effectName, false));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (int effect in bulletEffects)
        {
            switch (effect)
            {
                case (int)BULLETEFFECTS.SLOWOVERTIME: // Bullets slow over time.
                    gameObject.GetComponent<Rigidbody2D>().velocity /= 1.1f;
                    break;
                case (int)BULLETEFFECTS.EXPLODEONHIT: // Bullets explode on hit.
                    if (timer == 15)
                    {
                        exploSoin();
                        gameObject.GetComponent<Bullet_Movement>().KillBullet();
                    }
                    break;
            }
        }

        timer++;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        foreach (int effect in bulletEffects)
        {
            switch (effect)
            {
                case (int)BULLETEFFECTS.EXPLODEONHIT: // Bullets explode on hit.
                    exploSoin();
                    break;
            }
        }
    }

    // Methods for doing the various effects.
    void exploSoin()
    {
        GameObject splodo = Instantiate(EntityReferencerGuy.Instance.neutralExplosion, transform.position, Quaternion.Euler(0, 0, 0));
        splodo.transform.localScale = new Vector3(2, 2, 2);
        splodo.GetComponent<DealDamage>().finalDamageStat = gameObject.GetComponent<DealDamage>().GetDamageAmount();
    }
}
