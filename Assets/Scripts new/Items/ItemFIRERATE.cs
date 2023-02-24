using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFIRERATE : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Attack>().fireTimerLength -= 4;
    }

    public void Undo()
    {
        gameObject.GetComponent<Attack>().fireTimerLength += 4;
    }
}
