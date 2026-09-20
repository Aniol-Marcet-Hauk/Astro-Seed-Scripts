using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogGlitchWhenHit : MonoBehaviour
{
    public MonoBehaviour glitchEffect;
    public float glitchDuration = 0.2f;
    private void Start()
    {
        GameManager.gameManager.onHit += OnPlayerHit;
    }

  

    private void OnPlayerHit(bool isHit)
    {
        if (isHit)
        {
            glitchEffect.enabled = true;
            Invoke("Stop", glitchDuration);

        }
    }

    void Stop()
    {
        glitchEffect.enabled = false;
    }
   
}
