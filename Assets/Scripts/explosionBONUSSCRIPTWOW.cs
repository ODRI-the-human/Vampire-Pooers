using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBONUSSCRIPTWOW : MonoBehaviour
{
    public GameObject explosionAudio;

    // Update is called once per frame, but this is actually a start so go fuck yourself idiot
    void Start()
    {
        Instantiate(explosionAudio);
    }
}
