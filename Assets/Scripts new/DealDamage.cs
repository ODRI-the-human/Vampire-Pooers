using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [HideInInspector] public float finalDamageStat;
    public float procCoeff;
    public float damageBase;
    public float damageMult;
    public float finalDamageMult = 1;
    public float knockBackCoeff = 1;
    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Hostile" || gameObject.tag == "Player")
        {
            owner = gameObject;
        }
        finalDamageStat = damageBase * damageMult * finalDamageMult;
    }

    void Update()
    {
        finalDamageStat = damageBase * damageMult * finalDamageMult;
    }
}
