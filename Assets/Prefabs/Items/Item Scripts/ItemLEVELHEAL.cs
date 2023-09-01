using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLEVELHEAL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<LevelUp>().healMult += 1;
    }

    // Update is called once per frame
    void Undo()
    {
        gameObject.GetComponent<LevelUp>().healMult -= 1;
        Destroy(this);
    }
}
