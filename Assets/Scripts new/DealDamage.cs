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
    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Hostile" || gameObject.tag == "Player")
        {
            owner = gameObject;
        }
        finalDamageStat = damageBase * damageMult * finalDamageMult / finalDamageDIV;
    }

    void Update()
    {
        finalDamageStat = damageBase * damageMult * finalDamageMult / finalDamageDIV;
    }
}
