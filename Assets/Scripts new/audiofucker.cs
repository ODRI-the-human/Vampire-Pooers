using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiofucker : MonoBehaviour
{
    public AudioSource funnySound;
    public AudioReverbFilter reverb;
    int timer = 0;

    void FixedUpdate()
    {
        timer++;

        if (timer % 10 == 0)
        {
            funnySound.pitch = Random.Range(0.5f, 1.5f);
            funnySound.panStereo = Random.Range(-1, 1);
            reverb.density = Random.Range(0.5f, 1.5f);
            reverb.decayTime = Random.Range(0.5f, 1.5f);
        }
    }
}
