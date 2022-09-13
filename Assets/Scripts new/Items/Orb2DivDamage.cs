using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb2DivDamage : MonoBehaviour
{
    public int instances = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<DealDamage>().finalDamageMult = 0.25f * instances;
    }
}
