using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPIERCING : MonoBehaviour
{
    public int instances = 1;

    void Start()
    {
        gameObject.GetComponent<Bullet_Movement>().piercesLeft += 1;
    }

    public void Undo()
    {
        //nothin
    }
}
