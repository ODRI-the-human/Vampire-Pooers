using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMORESHOT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Attack>().noExtraShots++;
        gameObject.GetComponent<Attack>().cooldownFac *= 1.25f;
    }

    public void Undo()
    {
        gameObject.GetComponent<Attack>().noExtraShots--;
        gameObject.GetComponent<Attack>().cooldownFac /= 1.25f;
        Destroy(this);
    }
}