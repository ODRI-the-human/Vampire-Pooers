using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float finalDamageStat;
    public float procCoeff;
    public float damageBase;
    public float damageMult;
    public float finalDamageMult = 1;
    public float knockBackCoeff = 1;
    public float finalDamageDIV = 1;
    public float critProb = 0.05f;
    public float critMult = 2;
    public GameObject owner;

    public bool overwriteDamageCalc;

    void Awake()
    {
        critProb = 0.05f;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Hostile" || gameObject.tag == "Player")
        {
            owner = gameObject;
        }
        if (!overwriteDamageCalc)
        {
            finalDamageStat = damageBase * damageMult * finalDamageMult / finalDamageDIV;
        }
    }

    void Update()
    {
        if (!overwriteDamageCalc)
        {
            finalDamageStat = damageBase * damageMult * finalDamageMult / finalDamageDIV;
        }
    }
}
