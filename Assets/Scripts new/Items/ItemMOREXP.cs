using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMOREXP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<LevelUp>().xpMult += 0.3f;
    }

    // Update is called once per frame
    void Undo()
    {
        gameObject.GetComponent<LevelUp>().xpMult -= 0.3f;
    }
}
