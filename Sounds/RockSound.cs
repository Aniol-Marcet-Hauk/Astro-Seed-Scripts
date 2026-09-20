using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSound : MonoBehaviour, IAudioBall
{
    public AudioClip[] AudioClips;
    public Vector2 pitchRange;
    public AudioClip GetRockSound()
    {
        return AudioClips[Random.Range(0, AudioClips.Length-1)];
    }

    public Vector2 GetPitchRange()
    {
        return pitchRange;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
