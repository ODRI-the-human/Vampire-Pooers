using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public int level = 1;
    public int XP = 0;
    public GameObject PlayerXPAudio;

    public delegate void LevelBonus();
    public static LevelBonus levelEffects;

    void UpdateStats()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP += 5f;
        gameObject.GetComponent<HPDamageDie>().HP += 5f;
        gameObject.GetComponent<Attack>().fireTimerLength -= 0.3f;
        gameObject.GetComponent<NewPlayerMovement>().moveSpeed += 0.15f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "XP")
        {
            XP += 10;
            Instantiate(PlayerXPAudio);
            if (XP >= 20 * level)
            {
                level += 1;
                UpdateStats();
                LevelUp.levelEffects();
            }
        }
    }
}
