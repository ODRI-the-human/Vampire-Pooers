using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public float healMult = 1;
    GameObject master;

    void Start()
    {
        master = GameObject.Find("bigFuckingMasterObject");
    }

    public void Healo(float amount)
    {
        Debug.Log("Sprongly");
        gameObject.GetComponent<HPDamageDie>().HP += amount * healMult;
        master.GetComponent<showDamageNumbers>().showDamage(transform.position, amount * healMult, (int)DAMAGETYPES.HEAL);
    }
}
