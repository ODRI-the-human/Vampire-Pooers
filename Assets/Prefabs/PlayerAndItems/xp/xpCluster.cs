using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xpCluster : MonoBehaviour
{
    public int numberToSpawn = 25;
    public GameObject xp;
    float randPosAmt = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Instantiate(xp, transform.position + new Vector3(Random.Range(-randPosAmt, randPosAmt), Random.Range(-randPosAmt, randPosAmt), 0), Quaternion.Euler(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
