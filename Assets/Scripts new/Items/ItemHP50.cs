using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHP50 : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<HPDamageDie>().MaxHP += 50;
        gameObject.GetComponent<HPDamageDie>().HP += 50;
    }
}
