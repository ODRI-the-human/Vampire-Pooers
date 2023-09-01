using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHEALMLT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Healing>().healMult += 1;
    }

    public void Undo()
    {
        gameObject.GetComponent<Healing>().healMult -= 1;
        Destroy(this);
    }
}
