using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunnerManagement : MonoBehaviour
{
    public List<GameObject> gunners = new List<GameObject>();
    public List<int> gunnerIDs = new List<int>();
    bool isPlayerTeam = true;

    int numDamageBonuses = 0;
    int numHomingBonuses = 0;
    int numFireRateBonuses = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Hostile")
        {
            isPlayerTeam = false;
        }
    }

    void ApplyOwnOnDeaths()
    {
        foreach (GameObject gunner in gunners)
        {
            Destroy(gunner);
        }
    }

    public void AddNew(int item)
    {
        GameObject spawnedGuy = Instantiate(EntityReferencerGuy.Instance.normieFamiliar, transform.position, transform.rotation);
        spawnedGuy.GetComponent<familiarMovement>().owner = gameObject;
        spawnedGuy.GetComponent<DealDamage>().owner = spawnedGuy;
        spawnedGuy.GetComponent<Attack>().isPlayerTeam = isPlayerTeam;


        spawnedGuy.GetComponent<familiarMovement>().gunnerType = item;
        if (gunners.Count > 0)
        {
            spawnedGuy.GetComponent<familiarMovement>().toFollow = gunners[gunners.Count - 1];
        }
        else
        {
            spawnedGuy.GetComponent<familiarMovement>().toFollow = gameObject;
        }

        switch (item)
        {
            case (int)ITEMLIST.FAMILIAR:
                spawnedGuy.GetComponent<Attack>().attackAutomatically = false;
                gunners.Add(spawnedGuy);
                gunnerIDs.Add(item);
                numDamageBonuses++;
                break;
            case (int)ITEMLIST.HOMINGFAMILIAR:
                spawnedGuy.GetComponent<Attack>().attackAutomatically = false;
                gunners.Add(spawnedGuy);
                gunnerIDs.Add(item);
                numHomingBonuses++;
                break;
            case (int)ITEMLIST.AUTOFAMILIAR:
                spawnedGuy.GetComponent<Attack>().attackAutomatically = true;
                gunners.Add(spawnedGuy);
                gunnerIDs.Add(item);
                numFireRateBonuses++;
                break;
        }

        if (!isPlayerTeam)
        {
            spawnedGuy.GetComponent<Attack>().attackAutomatically = true;
        }

        foreach (GameObject gunner in gunners)
        {
            gunner.GetComponent<ItemHolder>().itemsHeld.Clear();
            switch (gunner.GetComponent<familiarMovement>().gunnerType)
            {
                case (int)ITEMLIST.FAMILIAR:
                    gunner.GetComponent<DealDamage>().finalDamageMult = 0.4f;
                    break;
                case (int)ITEMLIST.HOMINGFAMILIAR:
                    gunner.GetComponent<DealDamage>().finalDamageMult = 0.4f;
                    break;
                case (int)ITEMLIST.AUTOFAMILIAR:
                    gunner.GetComponent<DealDamage>().finalDamageMult = 0.3f;
                    break;
            }
        
            for (int i = 0; i < numHomingBonuses; i++)
            {
                gunner.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.HOMING);
            }
                
            gunner.GetComponent<DealDamage>().finalDamageMult *= 1 + 0.5f * numDamageBonuses;
            gunner.GetComponent<Attack>().cooldownFac = 1;

            for (int i = 0; i < numFireRateBonuses; i++)
            {
                gunner.GetComponent<Attack>().cooldownFac *= 0.9f;
            }
        }

        Debug.Log("you got famileiar this is bonekrs");
    }

    public void RemoveGunner(int item)
    {
        for (int i = 0; i < gunners.Count; i++)
        {
            if (i == item)
            {
                if (gunners.Count > 1)
                {
                    GameObject gunnerAfter = gunners[i + 1];
                    GameObject afterToFollow = gunners[i].GetComponent<familiarMovement>().toFollow;
                }

                Destroy(gunners[i]);
                gunners.RemoveAt(i);
                gunnerIDs.RemoveAt(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Undo()
    {
        ApplyOwnOnDeaths();
    }
}
