using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFIRERATE : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Attack>().fireRate += 1;
    }

    public void Undo()
    {
        gameObject.GetComponent<Attack>().fireRate -= 1;
        Destroy(this);
    }
}
