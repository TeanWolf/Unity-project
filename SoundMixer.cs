using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixer : MonoBehaviour
{
    public AudioMixer masterMixer;
    [SerializeField] private AudioSource Attacking;

    public void SetSoundVolume(float vol)
    {
        masterMixer.SetFloat("SoundVolume", vol);
        Attacking.Play();
    }
}
