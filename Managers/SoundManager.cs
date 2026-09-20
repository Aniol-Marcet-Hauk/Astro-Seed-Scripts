using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("UI Sounds")]
    public AudioClip buttonHover;
    public AudioClip buttonClick, 
        letterSound;

    [Header("UI Pitch and loudness")]
    [Range(0.5f, 1.5f)] public float uiPitchMin = 0.95f;
    [Range(0.5f, 1.5f)] public float uiPitchMax = 1.05f;
    [Range(0f, 1f)] public float uiVolume = .6f;
    private AudioSource audioSource;
    [Header("Output")]
    public AudioMixerGroup outputMixerGroup;

    void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;

        }
        else
        {
        
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

       audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip,float volume)
    {
        if (clip == null) return;

        audioSource.PlayOneShot(clip,volume);
      

    }

    // Play with a small randomized pitch
    private void PlaySoundUIWithRandomPitch(AudioClip clip)
    {
        PlaySoundWithRandomPitch(clip, uiPitchMin, uiPitchMax,uiVolume);
        //audioSource.pitch = previousPitch;
    }
    public void PlaySoundWithRandomPitch(AudioClip clip,float min,float max)
    {
       PlaySoundWithRandomPitch(clip, min, max, 1f);
        //audioSource.pitch = previousPitch;
    }
    public void PlaySoundWithRandomPitch(AudioClip clip, float min, float max, float volume)
    {
        if (clip == null || audioSource == null) return;
        float previousPitch = audioSource.pitch;
        float randomized = Random.Range(min, max);
        audioSource.pitch = randomized;
        audioSource.PlayOneShot(clip, volume);
        //audioSource.pitch = previousPitch;
    }


    // Convenience methods
    public void PlayButtonHover()
    {
        PlaySoundUIWithRandomPitch(buttonHover);
    }

    public void PlayButtonClick()
    {
        PlaySoundUIWithRandomPitch(buttonClick);
    }
    public void LetterSound()
    {
        PlaySoundWithRandomPitch(letterSound, 0.9f, 1.1f);
    }
}

