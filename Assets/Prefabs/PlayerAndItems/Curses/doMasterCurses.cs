using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doMasterCurses : MonoBehaviour
{
    public int numRoundsDropItemLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDropItemOnDeath(GameObject enemy)
    {
        if (numRoundsDropItemLeft > 0)
        {
            enemy.AddComponent<dropItemOnHit>();
            enemy.GetComponent<dropItemOnHit>().itemPedestal = gameObject.GetComponent<ThirdEnemySpawner>().itemPedestal;
        }
    }
}
