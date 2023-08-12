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
    public AbilityParams itemChestSpawn;

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
        gameObject.GetComponent<Attack>().cooldownFac *= 1f - effectMult * 0.03f;
        gameObject.GetComponent<NewPlayerMovement>().baseMoveSpeed += effectMult * 0.07f;

        if (itemChestSpawn != null)
        {
            itemChestSpawn.UseAttack(gameObject, null, transform.position, Vector2.zero, true, 0, false, true, false, true);
        }

        if(gameObject.GetComponent<ItemMORELEVELSTATS>() != null)
        {
            //gameObject.GetComponent<Attack>().levelDamageBonus += Mathf.Pow(gameObject.GetComponent<ItemMORELEVELSTATS>().instances, 1.323f) * effectMult * 5;
            gameObject.GetComponent<DealDamage>().damageBonus += 0.1f * effectMult * Mathf.Pow(gameObject.GetComponent<ItemMORELEVELSTATS>().instances, 1.2f);
            gameObject.GetComponent<HPDamageDie>().iFramesTimer += 5f * effectMult * Mathf.Pow(gameObject.GetComponent<ItemMORELEVELSTATS>().instances, 1.2f);
        }

        if (healMult > 0)
        {
            gameObject.GetComponent<Healing>().Healo(25 * healMult);
        }
    }

    public void GiveXP(int amount)
    {
        XP += Mathf.RoundToInt(amount * xpMult);
        if (gameObject.GetComponent<ItemHEALTHXP>() != null)
        {
            XP += Mathf.RoundToInt(amount * 2 * gameObject.GetComponent<ItemHEALTHXP>().instances * (gameObject.GetComponent<HPDamageDie>().MaxHP - gameObject.GetComponent<HPDamageDie>().HP) / gameObject.GetComponent<HPDamageDie>().MaxHP);
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
