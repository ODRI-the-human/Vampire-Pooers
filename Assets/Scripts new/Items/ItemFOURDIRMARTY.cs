using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFOURDIRMARTY : MonoBehaviour
{

    public int instances = 1;
    int garryFredricson;
    int prevAttackNo;

    void Start()
    {
        garryFredricson = 0;
    }

    void Update()
    {
        if (gameObject.GetComponent<Attack>().timesFired % (6 - instances) == 0 && garryFredricson == 0)
        {
            gameObject.GetComponent<Attack>().SpawnAttack((Mathf.PI) / 2);
            gameObject.GetComponent<Attack>().SpawnAttack(Mathf.PI);
            gameObject.GetComponent<Attack>().SpawnAttack(3 * (Mathf.PI) / 2);
            garryFredricson = 1;
            prevAttackNo = gameObject.GetComponent<Attack>().newAttack;
        }
        
        if (prevAttackNo != gameObject.GetComponent<Attack>().newAttack)
        {
            garryFredricson = 0;
        }

    }
}
