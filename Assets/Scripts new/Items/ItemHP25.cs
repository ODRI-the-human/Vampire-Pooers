using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHP25 : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP += 25;
        gameObject.GetComponent<HPDamageDie>().HP += 25;
    }

    public void Undo()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP -= 25;
        Destroy(this);
    }
}
