using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses : MonoBehaviour
{
    public int timer = 0;
    GameObject poisonIcon;
    GameObject bleedIcon;
    GameObject electricIcon;
    GameObject spawnedPoisonIcon;
    GameObject spawnedBleedIcon;
    GameObject spawnedElectricIcon;
    GameObject slowIcon;
    GameObject spawnedSlowIcon;
    public int poisonStacks;
    public int bleedStacks = 0;
    public bool hasElectric = false;
    public List<GameObject> electricDealers = new List<GameObject>(); 
    public int bleedTimer = 0;
    public bool isFrozen = false;
    public int freezeTimer = 0;
    public int freezeTimerLength = 100;
    public List<int> poisonTimers = new List<int>();
    public List<float> poisonDamages = new List<float>();
    public List<int> iconOrder = new List<int>();

    void Start()
    {
        poisonIcon = EntityReferencerGuy.Instance.poisonIcon;
        bleedIcon = EntityReferencerGuy.Instance.bleedIcon;
        electricIcon = EntityReferencerGuy.Instance.electricIcon;
        slowIcon = EntityReferencerGuy.Instance.slowIcon;
        if (gameObject.GetComponent<NewPlayerMovement>() != null)
        {
            spawnedSlowIcon = Instantiate(slowIcon);
            spawnedSlowIcon.GetComponent<Icons>().owner = gameObject;
            spawnedSlowIcon.GetComponent<Icons>().statusType = 3;
        }
        spawnedPoisonIcon = Instantiate(poisonIcon);
        spawnedPoisonIcon.GetComponent<Icons>().owner = gameObject;
        spawnedPoisonIcon.GetComponent<Icons>().statusType = 1;
        spawnedBleedIcon = Instantiate(bleedIcon);
        spawnedBleedIcon.GetComponent<Icons>().owner = gameObject;
        spawnedBleedIcon.GetComponent<Icons>().statusType = 0;
        spawnedElectricIcon = Instantiate(electricIcon);
        spawnedElectricIcon.GetComponent<Icons>().owner = gameObject;
        spawnedElectricIcon.GetComponent<Icons>().statusType = 2;
    }

    void FixedUpdate()
    {
        // Every 5 updates it checks if the enemies to deal electric are still alive, if not, removes them, and if none are alive it removes the status.
        if (timer % 5 == 0)
        {
            for (int i = 0; i < electricDealers.Count; i++)
            {
                if (electricDealers[i] == null)
                {
                    electricDealers.RemoveAt(i);
                }
            }

            if (electricDealers.Count == 0)
            {
                hasElectric = false;
            }
        }
        timer++;

        for (int i = 0; i < poisonTimers.Count; i++)
        {
            poisonTimers[i]++;

            if (poisonTimers[i] % 25 == 0)
            {
                gameObject.GetComponent<HPDamageDie>().Hurty(poisonDamages[i], false, true, 0, (int)DAMAGETYPES.POISON, true, null);
                //master.GetComponent<showDamageNumbers>().showDamage(transform.position, poisonDamages[i], (int)DAMAGETYPES.POISON, false);
            }

            if (poisonTimers[i] == 100)
            {
                poisonTimers.RemoveAt(i);
                poisonDamages.RemoveAt(i);
                poisonStacks--;
            }
        }

        bleedTimer++;

        if (bleedTimer % 10 == 0 && bleedStacks > 0)
        {
            gameObject.GetComponent<HPDamageDie>().Hurty(3 * bleedStacks, false, true, 0, (int)DAMAGETYPES.BLEED, true, null);
            //master.GetComponent<showDamageNumbers>().showDamage(transform.position, 3 * bleedStacks, (int)DAMAGETYPES.BLEED, false);
        }

        if (bleedTimer == 100)
        {
            bleedStacks = 0;
        }
    }
    
    public void TriggerPoison(float damageAmt)
    {
        poisonTimers.Add(0);
        poisonDamages.Add(damageAmt);
        if (!iconOrder.Contains(1))
        {
            iconOrder.Add(1);
        }
        poisonStacks++;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<TriggerPoison>() != null && gameObject.GetComponent<HPDamageDie>().iFrames < 0)
        {
            TriggerPoison(col.gameObject.GetComponent<TriggerPoison>().damageAmt);
        }

        if (col.gameObject.GetComponent<wapantCircle>() != null)
        {
            if (!iconOrder.Contains(3))
            {
                iconOrder.Add(3);
            }
        }
    }
}
