using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public float healMult = 1;
    public float healDiv = 1;
    GameObject master;

    void Start()
    {
        master = GameObject.Find("bigFuckingMasterObject");
    }

    public void newWaveEffects()
    {
        Healo(10);
    }

    public void Healo(float amount)
    {
        gameObject.GetComponent<HPDamageDie>().HP += amount * healMult / healDiv;
        master.GetComponent<showDamageNumbers>().showDamage(transform.position, amount * healMult / healDiv, (int)DAMAGETYPES.HEAL, false);
    }
}
