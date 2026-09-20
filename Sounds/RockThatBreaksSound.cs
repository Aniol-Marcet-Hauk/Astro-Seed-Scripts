using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class RockThatBreaksSound : MonoBehaviour, IAudioBall
{
    public AudioClip AudioClip;
    public Vector2 pitchRange;
    [Space]
    public AudioClip rockBreakingSound;
    public AudioClip rockAppearingSound;
    public AudioClip rockIdleSound;

    public AudioSource audioSource;
    public AudioSource idleAudioSource;
    public Vector2 pitchRangeIdle;
    private void Start()
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    public AudioClip GetRockSound()
    {
        return AudioClip;
    }

    public Vector2 GetPitchRange()
    {
        return pitchRange;
    }
    public void PlayBreaking()
    {
        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.PlayOneShot(rockBreakingSound);
    }
    public void PlayAppearing()
    {
        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.PlayOneShot(rockAppearingSound);
    }
    public void PlayIdleSound()
    {
       if(idleAudioSource != null && rockIdleSound != null)
        {
            idleAudioSource.pitch = Random.Range(pitchRangeIdle.x, pitchRangeIdle.y);
            if (!idleAudioSource.isPlaying)
            {
                idleAudioSource.clip = rockIdleSound;
                idleAudioSource.loop = true;
                idleAudioSource.Play();
            }
        }
    }
    public void StopIdleSound()
    {
        if(idleAudioSource != null)
        {
            idleAudioSource.Stop();
        }
    }
}
