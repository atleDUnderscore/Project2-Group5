using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string volumePref = "FirstPlay";
    private int firstPlayInt;
    public static float gameVolume;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
        gameVolume = volume;
    }

    private void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt == 0)
        {
            gameVolume = 1f;
            PlayerPrefs.SetFloat(volumePref, gameVolume);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            gameVolume = PlayerPrefs.GetFloat(volumePref);
        }

        
    }
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(volumePref, gameVolume);
    }
}
