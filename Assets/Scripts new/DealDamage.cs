using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float finalDamageStat;
    float procCoeff;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        procCoeff = 1;
        finalDamageStat = 50;
    }
}
