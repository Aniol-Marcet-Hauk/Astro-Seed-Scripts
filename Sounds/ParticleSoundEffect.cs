using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSoundEffect : MonoBehaviour
{
    public AudioSource collisionAudio;

    void OnParticleCollision(GameObject other)
    {
        if (collisionAudio != null)
        {
            collisionAudio.pitch = Random.Range(0.8f, 1.2f);
            collisionAudio.Play();

        }
    }
}
