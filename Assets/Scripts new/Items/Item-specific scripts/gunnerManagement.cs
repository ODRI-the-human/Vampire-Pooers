using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunnerManagement : MonoBehaviour
{
    public List<GameObject> gunners = new List<GameObject>();
    public List<int> gunnerIDs = new List<int>();

    int numDamageBonuses = 0;
    int numHomingBonuses = 0;
    int numFireRateBonuses = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddNew(int item)
    {
        GameObject spawnedGuy = Instantiate(EntityReferencerGuy.Instance.normieFamiliar, transform.position, transform.rotation);
        spawnedGuy.GetComponent<familiarMovement>().owner = gameObject;
        spawnedGuy.GetComponent<DealDamage>().owner = spawnedGuy;
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
                spawnedGuy.GetComponent<DealDamage>().finalDamageMult = 0.4f;
                gunners.Add(spawnedGuy);
                gunnerIDs.Add(item);
                numDamageBonuses++;
                break;
            case (int)ITEMLIST.HOMINGFAMILIAR:
                spawnedGuy.GetComponent<DealDamage>().finalDamageMult = 0.4f;
                gunners.Add(spawnedGuy);
                gunnerIDs.Add(item);
                numHomingBonuses++;
                break;
            case (int)ITEMLIST.AUTOFAMILIAR:
                spawnedGuy.GetComponent<DealDamage>().finalDamageMult = 0.3f;
                spawnedGuy.GetComponent<Attack>().getEnemyPos = true;
                spawnedGuy.GetComponent<Attack>().playerControlled = false;
                gunners.Add(spawnedGuy);
                gunnerIDs.Add(item);
                numFireRateBonuses++;
                break;
        }

        foreach (GameObject gunner in gunners)
        {
            gunner.GetComponent<ItemHolder>().itemsHeld.Clear();

            for (int i = 0; i < numHomingBonuses; i++)
            {
                gunner.GetComponent<ItemHolder>().itemsHeld.Add((int)ITEMLIST.HOMING);
            }
                
            gunner.GetComponent<DealDamage>().finalDamageMult *= 1 + 0.5f * numDamageBonuses;

            gunner.GetComponent<Attack>().fireTimerLengthMLT /= 0.5f * numFireRateBonuses;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
