using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDODGESPLOSION : MonoBehaviour
{

    GameObject dodgeSplosion;
    public int instances;

    // Start is called before the first frame update
    void Start()
    {
        dodgeSplosion = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().dodgeSplosion;
        instances = 1;
    }

    public void Splosm()
    {
        GameObject explodyDodge = Instantiate(dodgeSplosion, transform.position, transform.rotation);
        explodyDodge.transform.localScale *= 1.5f + 1.2f * instances;
    }
}