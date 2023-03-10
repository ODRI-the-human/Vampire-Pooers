using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomisePitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().pitch *= Random.Range(0.9f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
