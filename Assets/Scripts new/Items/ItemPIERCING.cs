using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPIERCING : MonoBehaviour
{
    public int instances = 1;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        DetermineShotRolls();
    }

    void DetermineShotRolls()
    {
        gameObject.GetComponent<Bullet_Movement>().piercesLeft += 1;
    }

    public void Undo()
    {
        Destroy(this);
    }
}
