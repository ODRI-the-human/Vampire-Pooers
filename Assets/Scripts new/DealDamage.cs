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

    // Start is called before the first frame update
    void Start()
    {
        finalDamageStat = damageBase * damageMult * finalDamageMult;
    }

    void Update()
    {
        finalDamageStat = damageBase * damageMult * finalDamageMult;
    }
}
