using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public int timer = 0;
    GameObject poisonIcon;
    GameObject bleedIcon;
    GameObject spawnedPoisonIcon;
    GameObject spawnedBleedIcon;
    public int poisonStacks;
    public int bleedStacks = 0;
    public int bleedTimer = 0;
    public List<int> poisonTimers = new List<int>();

    void Start()
    {
        poisonIcon = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().poisonIcon;
        bleedIcon = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().bleedIcon;
        spawnedPoisonIcon = Instantiate(poisonIcon);
        spawnedPoisonIcon.GetComponent<Icons>().owner = gameObject;
        spawnedPoisonIcon.GetComponent<Icons>().statusType = 1;
        spawnedBleedIcon = Instantiate(bleedIcon);
        spawnedBleedIcon.GetComponent<Icons>().owner = gameObject;
        spawnedBleedIcon.GetComponent<Icons>().statusType = 0;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < poisonTimers.Count; i++)
        {
            poisonTimers[i]++;

            if (poisonTimers[i] % 25 == 0)
            {
                gameObject.GetComponent<HPDamageDie>().HP -= 5;
            }

            if (poisonTimers[i] == 200)
            {
                poisonTimers.RemoveAt(i);
                poisonStacks--;
            }
        }

        bleedTimer++;

        if (bleedTimer % 10 == 0)
        {
            gameObject.GetComponent<HPDamageDie>().HP -= 3 * bleedStacks;
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
            poisonTimers.Add(1);
            poisonStacks++;
        }
    }
}
