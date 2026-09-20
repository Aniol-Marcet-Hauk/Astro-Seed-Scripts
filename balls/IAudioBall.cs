using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioBall
{
    AudioClip GetRockSound();
    Vector2 GetPitchRange();
}
