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
        if (gameObject.GetComponent<Attack>().timesFired % 4 == 0 && garryFredricson == 0)
        {
            for (int i = 1; i < instances * 4; i++)
            {
                gameObject.GetComponent<Attack>().currentAngle = i * (Mathf.PI) / (2 * instances);
                //Debug.Log("Family Guy: " + gameObject.GetComponent<Attack>().currentAngle.ToString());
                gameObject.GetComponent<Attack>().UseWeapon(true);
            }
            garryFredricson = 1;
            prevAttackNo = gameObject.GetComponent<Attack>().newAttack;
        }
        
        if (prevAttackNo != gameObject.GetComponent<Attack>().newAttack)
        {
            garryFredricson = 0;
        }

    }

    public void Undo()
    {
        Destroy(this);
    }
}
