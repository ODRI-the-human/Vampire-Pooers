using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDMGMLT2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<DealDamage>().damageMult += 1f;
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().damageMult -= 1f;
        Debug.Log("DogShit Valley");
    }
}
