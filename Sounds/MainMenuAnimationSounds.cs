using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationSounds : MonoBehaviour
{
    private SoundManager soundManager;

    public AudioClip lightSwitchSound,lightFullyOnSound;
    public AudioClip rocketFlySound;
    public AudioClip playOptionsMenuOpen,
        aMenuGettingOpen
        , titleAppear,
        playOptionsMenuClose;
    [Space]
    public float shakeTime, shakeAmp, shakeFreq, shakeTimeReduce, shakeTimeIncrease;

    void Start()
    {
        soundManager = SoundManager.instance;
    }

    public void TurnOnLightsSound()
    {
        soundManager.PlaySoundWithRandomPitch(lightSwitchSound,0.95f,1.05f);
    }
    public void TurnLightsFullyOnSound()
    {
        soundManager.PlaySound(lightFullyOnSound);
    }
   
    public void RocketFlySound()
    {
        soundManager.PlaySound(rocketFlySound);
    }
    public void PlayOptionsMenuOpenSound()
    {
        soundManager.PlaySound(playOptionsMenuOpen);
    }
    public void PlayOptionsMenuCloseSound()
    {
        soundManager.PlaySound(playOptionsMenuClose);
    }
    public void AMenuGettingOpenSound()
    {
        soundManager.PlaySoundWithRandomPitch(aMenuGettingOpen,0.95f,1.05f);

    }
    public void TitleAppearSound()
    {
        soundManager.PlaySound(titleAppear);
    }

    public void Shake()
    {
        //GameManager.gameManager.ShakeCam(shakeTime, shakeAmp, shakeFreq, shakeTimeReduce,shakeTimeIncrease);
    }



}
