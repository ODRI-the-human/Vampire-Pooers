using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseZapNoise : MonoBehaviour
{
    public AudioClip[] zappies;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().clip = zappies[Random.Range(0, 2)];
        gameObject.GetComponent<AudioSource>().Play();

    }
}
