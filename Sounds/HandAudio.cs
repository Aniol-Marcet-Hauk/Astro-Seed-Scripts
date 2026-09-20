using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HandAudio : MonoBehaviour
{
    public AudioClip[] activateSound;
    public AudioClip deactivateSound;

    public AudioClip grabSoundCling;
    public AudioClip releaseSound;
    

    [Space]
    public AudioClip activeHumLoop;
    public AudioClip windHumLoop;

    private AudioSource audioSource;
    private AudioSource loopSource;
    private AudioSource windLoopSource;

    [Header("Pitch")]
    public Vector2 pitchGrab = new Vector2(0.95f, 1.05f);
    public Vector2 pitchGrabCling = new Vector2(0.95f, 1.05f);

    [Space]
    public Vector2 pitchActivate = new Vector2(0.95f, 1.05f);
    [Header("Hum Controls")]
    [Space] public Vector2 humPitchRange = new Vector2(0.98f, 1.02f);

 

    [Range(0f, 1f)] public float humPitchBlend = 0.5f;
    public float humAndWindMin = 0.1f;
    private float extraRand = 0f;

    public Vector2 humVolumeRange = new Vector2(0.2f, 0.7f);
    public float humAdjustSpeed = 8f;

    [Space]
    [Space] public Vector2 windPitchRange = new Vector2(0.98f, 1.02f);




    public Vector2 windVolumeRange = new Vector2(0.2f, 0.7f);

   
    

    private bool isActive = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = SoundManager.instance.outputMixerGroup;
        audioSource.spatialBlend = 1f; // 3D sound
        audioSource.playOnAwake = false;
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.outputAudioMixerGroup = SoundManager.instance.outputMixerGroup;
        loopSource.spatialBlend = 1f;
        loopSource.playOnAwake = false;
        loopSource.loop = true;
        windLoopSource = gameObject.AddComponent<AudioSource>();
        windLoopSource.outputAudioMixerGroup = SoundManager.instance.outputMixerGroup;
        windLoopSource.spatialBlend = 1f;
        windLoopSource.playOnAwake = false;
        windLoopSource.loop = true;
        PlayWindHumLoop();
    }


    void Update()
    {
        // While active, drive the loop's pitch and volume based on the blend floats.
        if (isActive && loopSource != null && loopSource.clip != null && loopSource.isPlaying)
        {
            float targetPitch = Mathf.Lerp(humPitchRange.x, humPitchRange.y, Mathf.Clamp01(humPitchBlend));
            float targetVolume = Mathf.Lerp(humVolumeRange.x, humVolumeRange.y, Mathf.Clamp01(humPitchBlend));

            loopSource.pitch = Mathf.Lerp(loopSource.pitch, targetPitch+extraRand, Time.deltaTime * Mathf.Max(0.0001f, humAdjustSpeed));
            loopSource.volume = Mathf.Lerp(loopSource.volume, targetVolume, Time.deltaTime * Mathf.Max(0.0001f, humAdjustSpeed));
        }
        if( windLoopSource != null && windLoopSource.clip != null && windLoopSource.isPlaying)
        {
            float targetPitch = Mathf.Lerp(windPitchRange.x, windPitchRange.y, Mathf.Clamp01(humPitchBlend));
            float targetVolume = Mathf.Lerp(windVolumeRange.x, windVolumeRange.y, Mathf.Clamp01(humPitchBlend));
            if(targetVolume < humAndWindMin)
            {
               targetVolume = 0f;

            }
            windLoopSource.pitch = Mathf.Lerp(windLoopSource.pitch, targetPitch, Time.deltaTime * Mathf.Max(0.0001f, humAdjustSpeed));
            windLoopSource.volume = Mathf.Lerp(windLoopSource.volume, targetVolume, Time.deltaTime * Mathf.Max(0.0001f, humAdjustSpeed));
        }
    }
    public void PlayWindHumLoop()
    {
        windLoopSource.clip = windHumLoop;

        // Set immediate targets based on current blend values
        windLoopSource.pitch = Mathf.Lerp(windPitchRange.x, windPitchRange.y, Mathf.Clamp01(humPitchBlend));
        windLoopSource.volume = Mathf.Lerp(windVolumeRange.x, windVolumeRange.y, Mathf.Clamp01(humPitchBlend));

        windLoopSource.Play();
    }

    public void PlayRandomizedPitch(AudioClip clip, float min, float max)
    {
        if (clip == null) return;
        float randomized = Random.Range(min, max);
        audioSource.pitch = randomized;
        audioSource.PlayOneShot(clip);
    }
    public void PlayGrab()
    {

        PlayRandomizedPitch(grabSoundCling, pitchGrabCling.x, pitchGrabCling.y);
    }

    public void PlayRelease()
    {
        audioSource.PlayOneShot(releaseSound);
    }
    public void PlayActivate()
    {
        if (isActive) return;
        isActive = true;    
        PlayRandomizedPitch(activateSound[Random.Range(0,activateSound.Length)], pitchActivate.x, pitchActivate.y);

        if (activeHumLoop == null) return;
        extraRand = Random.Range(-0.05f, 0.05f);
        loopSource.clip = activeHumLoop;

        // Set immediate targets based on current blend values
        loopSource.pitch = Mathf.Lerp(humPitchRange.x, humPitchRange.y, Mathf.Clamp01(humPitchBlend));
        loopSource.volume = Mathf.Lerp(humVolumeRange.x, humVolumeRange.y, Mathf.Clamp01(humPitchBlend));

        loopSource.Play();
    }
    public void PlayDeactivate()
    {
        if (!isActive) return;
        isActive = false;

        loopSource.Stop();
        PlayRandomizedPitch(deactivateSound, pitchActivate.x, pitchActivate.y);
    }

    // External setters for updating the hum parameters from other scripts (clamped to [0,1]).
    public void SetHumPitchBlend(float normalized)
    {
        humPitchBlend = Mathf.Clamp01(normalized);
    }

   
}
