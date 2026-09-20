using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class volumeScript : MonoBehaviour
{
    public AudioMixer am;
    public Slider musicS,soundsS;

    // Update is called once per frame
    private void Start()
    {
        LoadPlayerPrefsVolume();
    }
    private void LoadPlayerPrefsVolume()
    {  
        musicS.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundsS.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        PlayerPrefs.Save();
    }
    public void SetMusicVol()
    {
        am.SetFloat("MUSIC", Mathf.Log10(musicS.value)*20f);
        PlayerPrefs.SetFloat("MusicVolume", musicS.value);
    }
    public void SetSoundsVol()
    {
        am.SetFloat("SFX", Mathf.Log10(soundsS.value)*20f);
        PlayerPrefs.SetFloat("SFXVolume", soundsS.value);
        PlayerPrefs.Save();
    }
}
