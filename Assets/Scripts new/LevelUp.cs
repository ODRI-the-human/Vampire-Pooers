using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public int level = 1;
    public int effectMult = 1;
    public int XP = 0;
    public float nextXP;
    public GameObject PlayerXPAudio;

    public delegate void LevelBonus();
    public static LevelBonus levelEffects;

    void Update()
    {
        nextXP = level * 40 + 10 * Mathf.Pow(level, 2);
        if (XP >= nextXP)
        {
            level += 1;
            UpdateStats();
            LevelUp.levelEffects();
        }
    }

    void UpdateStats()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP += effectMult * 5f;
        gameObject.GetComponent<HPDamageDie>().HP += effectMult * 5f;
        gameObject.GetComponent<Attack>().fireTimerLength -= effectMult * 0.3f;
        gameObject.GetComponent<NewPlayerMovement>().moveSpeed += effectMult * 0.15f;

        if(gameObject.GetComponent<ItemMORELEVELSTATS>() != null)
        {
            gameObject.GetComponent<Attack>().levelDamageBonus += gameObject.GetComponent<ItemMORELEVELSTATS>().instances * effectMult * 5f;
            gameObject.GetComponent<DealDamage>().damageBase += gameObject.GetComponent<ItemMORELEVELSTATS>().instances * effectMult * 5f;
            gameObject.GetComponent<HPDamageDie>().iFramesTimer += gameObject.GetComponent<ItemMORELEVELSTATS>().instances * effectMult * 2.5f;
            gameObject.GetComponent<Attack>().scaleAddMult += gameObject.GetComponent<ItemMORELEVELSTATS>().instances * effectMult * 0.2f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "XP")
        {
            XP += 10;
            Instantiate(PlayerXPAudio);
        }
    }
}
