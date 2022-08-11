using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITEMExtraShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Attack>().noExtraShots++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
