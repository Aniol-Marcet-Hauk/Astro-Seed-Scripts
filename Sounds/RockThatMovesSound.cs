using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class RockThatMovesSound : MonoBehaviour , IAudioBall
{
    public AudioClip audioClip;
    public Vector2 pitchRange;
    [Space]
    public AudioClip moving;

    private AudioSource audioSource;
    public float min, max;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public AudioClip GetRockSound()
    {
        return audioClip;
    }

    public Vector2 GetPitchRange()
    {
        return pitchRange;
    }
    public void PlayMoving()
    {
         
        
        float randomized = Random.Range(min, max);
        audioSource.pitch = randomized;
        audioSource.PlayOneShot(moving);
    }
}
