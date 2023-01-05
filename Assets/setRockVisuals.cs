using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setRockVisuals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int bumBoy = Random.Range(0, 4);

        transform.rotation = Quaternion.Euler(180, 0, 90 * bumBoy);
    }
}
