using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public int level = 0;
    public int XP = 0;
    public GameObject PlayerXPAudio;

    void UpdateStats()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP += 5;
        gameObject.GetComponent<HPDamageDie>().HP += 5;
        gameObject.GetComponent<DealDamage>().damageBase += 5;
        gameObject.GetComponent<Attack>().fireTimerLength /= (1 + 0.03f * level);
        gameObject.GetComponent<NewPlayerMovement>().moveSpeed += 0.5f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "XP")
        {
            XP += 10;
            Instantiate(PlayerXPAudio);
            // levelling up
            //if (XP >= 180* (level - 1) + levelConstantMul * Mathf.Pow(1.5f, 1.1f * (level-1)))
            if (XP >= 50 * (level + 1) + 2.5f * Mathf.Pow(1.8f, 1.1f * (level + 1)))
            {
                level += 1;
                UpdateStats();
            }
        }
    }
}
