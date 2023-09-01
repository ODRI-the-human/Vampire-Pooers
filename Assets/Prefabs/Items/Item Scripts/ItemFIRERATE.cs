using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFIRERATE : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Attack>().cooldownFacIndiv[0] *= 0.9f;
    }

    public void Undo()
    {
        gameObject.GetComponent<Attack>().cooldownFacIndiv[0] /= 0.9f;
        Destroy(this);
    }
}
