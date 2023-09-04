using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBOUNCY : ItemScript
{
    public int bouncesLeft;

    void Start()
    {
        DetermineShotRolls();
    }

    void DetermineShotRolls()
    {
        bouncesLeft = instances;
        if (gameObject.GetComponent<meleeGeneral>() != null)
        {
            gameObject.GetComponent<DealDamage>().massCoeff *= 1 + 0.5f * instances;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<meleeGeneral>() != null) // For applying melee weapons' unique bouncy
        {
            collision.gameObject.AddComponent<hitIfKBVecHigh>();
            GameObject master = EntityReferencerGuy.Instance.master;
        }
    }

    public void Undo()
    {
        Destroy(this);
    }
}
