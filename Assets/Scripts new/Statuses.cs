using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses : MonoBehaviour
{
    public List<int> statusOrders = new List<int>();
    public int[] statusStacks = new int[4] { 0, 0, 0, 0 };
    public List<GameObject> spawnedIcons = new List<GameObject>();

    int timer;
    public GameObject iconPrefab;

    // Bleed
    public int bleedStacks;
    int bleedTimer = 5000;

    // Poison
    public List<int> poisonTimers = new List<int>();
    public List<float> poisonDamages = new List<float>();
    public int poisonStacks;

    // Electric
    public bool hasElectric = false;
    public List<GameObject> electricDealers = new List<GameObject>();

    // Slow
    public bool hasSlow = false;
    int slowTimer = 5000;

    void Start()
    {
        iconPrefab = EntityReferencerGuy.Instance.statusIcon;
    }

    public void SpawnNewIcon(int statusType)
    {
        bool iconExists = false;
        for (int i = 0; i < statusOrders.Count; i++)
        {
            if (statusOrders[i] == statusType)
            {
                iconExists = true;
            }
        }
        
        if (!iconExists)
        {
            GameObject newIcon = Instantiate(iconPrefab);
            spawnedIcons.Add(newIcon);
            newIcon.transform.SetParent(gameObject.transform);
            newIcon.GetComponent<Icons>().statusType = statusType;
            statusOrders.Add(statusType);
        }
    }

    public void RemoveIcon(int statusType)
    {
        for (int i = 0; i < statusOrders.Count; i++)
        {
            if (statusOrders[i] == statusType)
            {
                statusOrders.RemoveAt(i);
                spawnedIcons[i].GetComponent<Icons>().Die();
                spawnedIcons.RemoveAt(i);
            }
        }

        foreach (GameObject icon in spawnedIcons)
        {
            icon.SendMessage("GetNewPos");
        }
    }

    // Covers both adding the initial stack of a status and increasing number of stacks
    public void AddStatus(int statusType, float damageAmt, GameObject responsibleObj)
    {
        switch (statusType)
        {
            case (int)STATUSES.BLEED:
                // For bleed, increment stacks and refresh timer.
                bleedTimer = 0;
                bleedStacks += 1;
                break;
            case (int)STATUSES.POISON:
                // For poison, increment stacks and the damage amount of the source.
                poisonDamages.Add(damageAmt);
                poisonTimers.Add(0);
                break;
            case (int)STATUSES.ELECTRIC:
                // For electric, just add the status if the thing doesn't already have it, else do nothing.
                electricDealers.Add(responsibleObj);
                break;
            case (int)STATUSES.SLOW:
                if (!hasSlow)
                {
                    gameObject.GetComponent<NewPlayerMovement>().speedDiv *= 2;
                    hasSlow = true;
                }
                slowTimer = 0;
                // For slow, just add the status if the thing doesn't already have it, else refresh timer.
                break;
        }

        SpawnNewIcon(statusType);
    }

    public void FixedUpdate()
    {
        // Electric; checks if the dealer of electric is null, if no electric dealers are present it removes the icon.
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
                RemoveIcon((int)STATUSES.ELECTRIC);
                hasElectric = false;
            }
        }

        // Poison
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
            }

            if (poisonTimers.Count == 0)
            {
                RemoveIcon((int)STATUSES.POISON);
            }
        }

        // Bleed
        if (bleedTimer % 10 == 0)
        {
            if (bleedTimer <= 100)
            {
                gameObject.GetComponent<HPDamageDie>().Hurty(3 * bleedStacks, false, true, 0, (int)DAMAGETYPES.BLEED, true, null);
            }
            else
            {
                RemoveIcon((int)STATUSES.BLEED);
                bleedStacks = 0;
            }
        }

        // Slow
        if (hasSlow)
        {
            if (slowTimer > 100)
            {
                RemoveIcon((int)STATUSES.SLOW);
                gameObject.GetComponent<NewPlayerMovement>().speedDiv /= 2;
                hasSlow = false;
            }
        }

        // Setting no. of stacks of each status, for use by the icon. For statuses that don't stack, just set it to 0
        statusStacks[0] = bleedStacks;
        statusStacks[1] = poisonDamages.Count;
        statusStacks[2] = 0;
        statusStacks[3] = 0;

        timer++;
        bleedTimer++;
        slowTimer++;
    }

    //public int timer = 0;
    //GameObject poisonIcon;
    //GameObject bleedIcon;
    //GameObject electricIcon;
    //GameObject spawnedPoisonIcon;
    //GameObject spawnedBleedIcon;
    //GameObject spawnedElectricIcon;
    //GameObject slowIcon;
    //GameObject spawnedSlowIcon;
    //public int poisonStacks;
    //public int bleedStacks = 0;
    //public bool hasElectric = false;
    //public List<GameObject> electricDealers = new List<GameObject>(); 
    //public int bleedTimer = 0;
    //public bool isFrozen = false;
    //public int freezeTimer = 0;
    //public int freezeTimerLength = 100;
    //public List<int> poisonTimers = new List<int>();
    //public List<float> poisonDamages = new List<float>();
    //public List<int> iconOrder = new List<int>();

    //void Start()
    //{
    //    poisonIcon = EntityReferencerGuy.Instance.poisonIcon;
    //    bleedIcon = EntityReferencerGuy.Instance.bleedIcon;
    //    electricIcon = EntityReferencerGuy.Instance.electricIcon;
    //    slowIcon = EntityReferencerGuy.Instance.slowIcon;
    //    if (gameObject.GetComponent<NewPlayerMovement>() != null)
    //    {
    //        spawnedSlowIcon = Instantiate(slowIcon);
    //        spawnedSlowIcon.GetComponent<Icons>().owner = gameObject;
    //        spawnedSlowIcon.GetComponent<Icons>().statusType = 3;
    //    }
    //    spawnedPoisonIcon = Instantiate(poisonIcon);
    //    spawnedPoisonIcon.GetComponent<Icons>().owner = gameObject;
    //    spawnedPoisonIcon.GetComponent<Icons>().statusType = 1;
    //    spawnedBleedIcon = Instantiate(bleedIcon);
    //    spawnedBleedIcon.GetComponent<Icons>().owner = gameObject;
    //    spawnedBleedIcon.GetComponent<Icons>().statusType = 0;
    //    spawnedElectricIcon = Instantiate(electricIcon);
    //    spawnedElectricIcon.GetComponent<Icons>().owner = gameObject;
    //    spawnedElectricIcon.GetComponent<Icons>().statusType = 2;
    //}

    //void FixedUpdate()
    //{
    //    // Every 5 updates it checks if the enemies to deal electric are still alive, if not, removes them, and if none are alive it removes the status.
    //    if (timer % 5 == 0)
    //    {
    //        for (int i = 0; i < electricDealers.Count; i++)
    //        {
    //            if (electricDealers[i] == null)
    //            {
    //                electricDealers.RemoveAt(i);
    //            }
    //        }

    //        if (electricDealers.Count == 0)
    //        {
    //            hasElectric = false;
    //        }
    //    }
    //    timer++;

    //    for (int i = 0; i < poisonTimers.Count; i++)
    //    {
    //        poisonTimers[i]++;

    //        if (poisonTimers[i] % 25 == 0)
    //        {
    //            gameObject.GetComponent<HPDamageDie>().Hurty(poisonDamages[i], false, true, 0, (int)DAMAGETYPES.POISON, true, null);
    //            //master.GetComponent<showDamageNumbers>().showDamage(transform.position, poisonDamages[i], (int)DAMAGETYPES.POISON, false);
    //        }

    //        if (poisonTimers[i] == 100)
    //        {
    //            poisonTimers.RemoveAt(i);
    //            poisonDamages.RemoveAt(i);
    //            poisonStacks--;
    //        }
    //    }

    //    bleedTimer++;

    //    if (bleedTimer % 10 == 0 && bleedStacks > 0)
    //    {
    //        gameObject.GetComponent<HPDamageDie>().Hurty(3 * bleedStacks, false, true, 0, (int)DAMAGETYPES.BLEED, true, null);
    //        //master.GetComponent<showDamageNumbers>().showDamage(transform.position, 3 * bleedStacks, (int)DAMAGETYPES.BLEED, false);
    //    }

    //    if (bleedTimer == 100)
    //    {
    //        bleedStacks = 0;
    //    }
    //}

    //public void TriggerPoison(float damageAmt)
    //{
    //    poisonTimers.Add(0);
    //    poisonDamages.Add(damageAmt);
    //    if (!iconOrder.Contains(1))
    //    {
    //        iconOrder.Add(1);
    //    }
    //    poisonStacks++;
    //}

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.GetComponent<TriggerPoison>() != null && gameObject.GetComponent<HPDamageDie>().iFrames < 0)
    //    {
    //        TriggerPoison(col.gameObject.GetComponent<TriggerPoison>().damageAmt);
    //    }

    //    if (col.gameObject.GetComponent<wapantCircle>() != null)
    //    {
    //        if (!iconOrder.Contains(3))
    //        {
    //            iconOrder.Add(3);
    //        }
    //    }
    //}
}
