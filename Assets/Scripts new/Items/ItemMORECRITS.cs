using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMORECRITS : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<DealDamage>().critProb += 0.2f;
    }

    public void Undo()
    {
        gameObject.GetComponent<DealDamage>().critProb -= 0.2f;
        Destroy(this);
    }
}
