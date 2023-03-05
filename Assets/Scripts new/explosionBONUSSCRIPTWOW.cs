using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBONUSSCRIPTWOW : MonoBehaviour
{
    public GameObject explosionAudio;

    // Update is called once per frame, but this is actually a start so go fuck yourself nerd
    void Start()
    {
        Instantiate(explosionAudio);
        GameObject cameron = GameObject.Find("Main Camera");
        cameron.GetComponent<cameraMovement>().CameraShake(Mathf.RoundToInt(transform.localScale.x * 10));
    }
}
