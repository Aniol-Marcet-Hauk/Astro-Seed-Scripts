using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BodySound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clipBody;
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);
    public float strengthMultiplier = 0.1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomizedPitch(AudioClip clip, float min, float max,float volume)
    {
        if (clip == null) return;
        float randomized = Random.Range(min, max);
        audioSource.pitch = randomized;
        audioSource.PlayOneShot(clip, volume);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float volume = Mathf.Clamp01(collision.relativeVelocity.magnitude*strengthMultiplier);

        PlayRandomizedPitch(clipBody, pitchRange.x, pitchRange.y, volume);
    }


}
