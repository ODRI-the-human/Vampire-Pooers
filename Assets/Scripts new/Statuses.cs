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
    GameObject master;
    public int poisonStacks;
    public int bleedStacks = 0;
    public int hasElectric = 0;
    public int bleedTimer = 0;
    public List<int> poisonTimers = new List<int>();
    public List<int> iconOrder = new List<int>();

    void Start()
    {
        master = GameObject.Find("bigFuckingMasterObject");
        poisonIcon = master.GetComponent<EntityReferencerGuy>().poisonIcon;
        bleedIcon = master.GetComponent<EntityReferencerGuy>().bleedIcon;
        electricIcon = master.GetComponent<EntityReferencerGuy>().electricIcon;
        slowIcon = master.GetComponent<EntityReferencerGuy>().slowIcon;
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
        for (int i = 0; i < poisonTimers.Count; i++)
        {
            poisonTimers[i]++;

            if (poisonTimers[i] % 25 == 0)
            {
                gameObject.GetComponent<HPDamageDie>().HP -= 5;
                master.GetComponent<showDamageNumbers>().showDamage(transform.position, 5, (int)DAMAGETYPES.POISON);
            }

            if (poisonTimers[i] == 100)
            {
                poisonTimers.RemoveAt(i);
                poisonStacks--;
            }
        }

        bleedTimer++;

        if (bleedTimer % 10 == 0 && bleedStacks > 0)
        {
            gameObject.GetComponent<HPDamageDie>().HP -= 3 * bleedStacks;
            master.GetComponent<showDamageNumbers>().showDamage(transform.position, 3 * bleedStacks, (int)DAMAGETYPES.BLEED);
        }

        if (bleedTimer == 100)
        {
            bleedStacks = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<TriggerPoison>() != null)
        {
            poisonTimers.Add(0);
            if (!iconOrder.Contains(1))
            {
                iconOrder.Add(1);
            }
            poisonStacks++;
        }
    }
}
