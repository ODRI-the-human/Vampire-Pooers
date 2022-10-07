using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFIRERATE : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Attack>().fireTimerLength -= 4;
    }

    public void Undo()
    {
        gameObject.GetComponent<Attack>().fireTimerLength += 4;
    }
}
