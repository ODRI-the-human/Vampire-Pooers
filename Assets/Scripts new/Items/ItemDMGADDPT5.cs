using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDMGADDPT5 : MonoBehaviour
{

    public bool runStart = true;


    // Start is called before the first frame update
    void Awake()
    {
        if (!gameObject.GetComponent<DealDamage>().isBulletClone)
        {
            gameObject.GetComponent<DealDamage>().damageBase += 25f;
        }
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().damageBase -= 25f;
        Destroy(this);
    }
}
