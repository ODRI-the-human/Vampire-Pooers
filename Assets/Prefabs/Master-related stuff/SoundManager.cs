using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip clip, float randPitchAmt)
    {
        effectsSource.time = 0;
        effectsSource.clip = clip;
        effectsSource.Play();
        effectsSource.pitch = Random.Range(1f - randPitchAmt, 1f + randPitchAmt);
    }
}
