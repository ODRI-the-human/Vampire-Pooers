using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int finalDamageStat;
    float procCoeff;

    // Start is called before the first frame update
    void Start()
    {
        procCoeff = 1;
        finalDamageStat = 50;
    }
}
