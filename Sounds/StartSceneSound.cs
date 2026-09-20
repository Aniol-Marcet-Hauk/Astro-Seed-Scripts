using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneSound : MonoBehaviour
{
    // Start is called before the first frame update
    private SoundManager soundManager;
    public AudioClip sSound;
    void Start()
    {
        soundManager = SoundManager.instance;
    }


    public void PlayStartSceneSound()
    {
        soundManager.PlaySound(sSound);
    }
}
