using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBONUSSCRIPTWOW : MonoBehaviour
{
    public GameObject explosionAudio;

    // Update is called once per frame, but this is actually a start so go fuck yourself nerd
    void Start()
    {
        transform.localScale = (2f + gameObject.GetComponent<DealDamage>().finalDamageStat / 150f) * new Vector3(1, 1, 1);
        gameObject.GetComponent<DealDamage>().massCoeff = transform.localScale.x / 2f;
        Instantiate(explosionAudio);
        GameObject cameron = GameObject.Find("Main Camera");
        cameron.GetComponent<cameraMovement>().CameraShake(Mathf.RoundToInt(transform.localScale.x * 10));
        gameObject.GetComponent<DealDamage>().damageType = (int)DAMAGETYPES.EXPLOSION;
    }
}
