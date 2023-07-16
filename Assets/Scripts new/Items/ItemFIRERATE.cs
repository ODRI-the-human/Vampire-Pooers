using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFIRERATE : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Attack>().fireRate += 0.5f;
    }

    public void Undo()
    {
        gameObject.GetComponent<Attack>().fireRate -= 0.5f;
        Destroy(this);
    }
}
