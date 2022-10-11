using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBETTERLEVEL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<LevelUp>().effectMult += 2;
    }

    public void Undo()
    {
        gameObject.GetComponent<LevelUp>().effectMult -= 2;
    }
}
