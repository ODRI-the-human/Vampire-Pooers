using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public int level = 1;
    public int effectMult = 1;
    public float xpMult = 1;
    public int XP = 0;
    public float healMult = 0;
    public float nextXP;
    public GameObject PlayerXPAudio;

    void Start()
    {
        
    }

    void Update()
    {
        nextXP = level * 40 + 10 * Mathf.Pow(level, 2);
        if (XP >= nextXP)
        {
            level += 1;
            SendMessage("LevelEffects");
        }
    }

    void LevelEffects()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP += effectMult * 5f;
        gameObject.GetComponent<HPDamageDie>().HP += effectMult * 5f;
        gameObject.GetComponent<Attack>().fireTimerLength -= effectMult * 0.3f;
        gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed += effectMult * 0.07f;

        if(gameObject.GetComponent<ItemMORELEVELSTATS>() != null)
        {
            gameObject.GetComponent<Attack>().levelDamageBonus += Mathf.Pow(gameObject.GetComponent<ItemMORELEVELSTATS>().instances, 1.323f) * effectMult * 5;
            gameObject.GetComponent<DealDamage>().damageBase += Mathf.Pow(gameObject.GetComponent<ItemMORELEVELSTATS>().instances, 1.323f) * effectMult * 5;
            gameObject.GetComponent<HPDamageDie>().iFramesTimer += Mathf.Pow(gameObject.GetComponent<ItemMORELEVELSTATS>().instances, 1.323f) * effectMult * 5;
            gameObject.GetComponent<Attack>().scaleAddMult += Mathf.Pow(gameObject.GetComponent<ItemMORELEVELSTATS>().instances, 1.323f) * effectMult * 0.015f;
        }

        if (healMult > 0)
        {
            gameObject.GetComponent<Healing>().Healo(25 * healMult);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "XP")
        {
            XP += Mathf.RoundToInt(10 * xpMult);
            Instantiate(PlayerXPAudio);
        }
    }
}
