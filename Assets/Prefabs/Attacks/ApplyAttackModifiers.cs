using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ApplyAttackModifiers : MonoBehaviour
{
    public string[] effectNames;
    public List<int> attackEffects = new List<int>();
    int timer = 0;

    void Start()
    {
        foreach (string effectName in effectNames)
        {
            attackEffects.Add((int)Enum.Parse(typeof(ATTACKMODIFIERS), effectName, false));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (int effect in attackEffects)
        {
            switch (effect)
            {
                case (int)ATTACKMODIFIERS.EXPLODEONHIT: // Bullets explode on hit.
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
        if (col.gameObject.tag == "Wall")
        {
            GameObject owner = gameObject.GetComponent<DealDamage>().owner;
            owner.GetComponent<ItemHolder2>().OnWallHits(col.gameObject, gameObject);

            foreach (int effect in attackEffects)
            {
                switch (effect)
                {
                    case (int)ATTACKMODIFIERS.EXPLODEONHIT: // Bullets explode on hit.
                        exploSoin();
                        break;
                }
            }
        }
    }

    public void ModifierOnHits(GameObject victim, GameObject owner)
    {
        foreach (int effect in attackEffects)
        {
            switch (effect)
            {
                case (int)ATTACKMODIFIERS.DEALLARGEKNOCKBACK: // Bullets explode on hit.
                    victim.GetComponent<NewPlayerMovement>().knockBackVector *= 5f;
                    if (gameObject.GetComponent<checkAllLazerPositions>() != null)
                    {
                        victim.GetComponent<NewPlayerMovement>().knockBackVector = gameObject.GetComponent<DealDamage>().GetDamageAmount() * gameObject.GetComponent<checkAllLazerPositions>().vecToMove;
                        //Debug.Log("knog bag: " + victim.GetComponent<NewPlayerMovement>().knockBackVector.magnitude);
                    }
                    victim.AddComponent<hitIfKBVecHigh>();
                    victim.GetComponent<hitIfKBVecHigh>().responsible = gameObject;
                    break;
                case (int)ATTACKMODIFIERS.HEALONHIT:
                    owner.GetComponent<Healing>().Healo(5);
                    break;
            }
        }
    }

    //void RollOnHit(GameObject[] gos)
    //{
    //    GameObject victim = gos[0];
    //    GameObject dealer = gos[1];

    //    foreach (int effect in attackEffects)
    //    {
    //        switch (effect)
    //        {
    //            case (int)ATTACKMODIFIERS.DEALLARGEKNOCKBACK: // Bullets explode on hit.
    //                victim.GetComponent<NewPlayerMovement>().knockBackVector *= 5f;
    //                if (gameObject.GetComponent<checkAllLazerPositions>() != null)
    //                {
    //                    victim.GetComponent<NewPlayerMovement>().knockBackVector = gameObject.GetComponent<DealDamage>().GetDamageAmount() * gameObject.GetComponent<checkAllLazerPositions>().vecToMove;
    //                    //Debug.Log("knog bag: " + victim.GetComponent<NewPlayerMovement>().knockBackVector.magnitude);
    //                }
    //                victim.AddComponent<hitIfKBVecHigh>();
    //                victim.GetComponent<hitIfKBVecHigh>().responsible = gameObject;
    //                break;
    //        }
    //    }
    //}

    // Methods for doing the various effects.
    void exploSoin()
    {
        GameObject splodo = Instantiate(EntityReferencerGuy.Instance.neutralExplosion, transform.position, Quaternion.Euler(0, 0, 0));
        splodo.transform.localScale = new Vector3(2, 2, 2);
        splodo.GetComponent<DealDamage>().finalDamageStat = gameObject.GetComponent<DealDamage>().GetDamageAmount();
    }
}
