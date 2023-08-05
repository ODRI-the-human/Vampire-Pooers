using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    public AudioClip[] clips;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayTypicalSound(int ID) // For playing sounds that happen all the time, like hitmarkers, crit sound, etc. ID as per COMMONSNDCLPS enum.
    {
        PlaySound(clips[ID]);
    }

    public void PlaySound(AudioClip clip)
    {
        //effectsSource.time = 0;
        //effectsSource.clip = clip;
        effectsSource.PlayOneShot(clip);
        effectsSource.pitch = Random.Range(1f - 0.05f, 1f + 0.05f);
    }
}
