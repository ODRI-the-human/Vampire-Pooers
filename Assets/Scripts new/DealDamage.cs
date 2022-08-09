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
        finalDamageStat = 50;
        procCoeff = 1;
    }
}
