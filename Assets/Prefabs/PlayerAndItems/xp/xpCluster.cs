using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xpCluster : MonoBehaviour
{
    public int numberToSpawn = 25;
    public GameObject xp;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Instantiate(xp, transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0), Quaternion.Euler(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
