using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicMixer : MonoBehaviour
{
    public AudioMixer masterMixer;
    private float volume;
    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volume = PlayerPrefs.GetFloat("volume");
        }
        else
        {

        }
    }
    public void SetMusicVolume(float vol)
    {
        masterMixer.SetFloat("volume", vol);
        volume = vol;
        PlayerPrefs.SetFloat("volume", volume);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

}
