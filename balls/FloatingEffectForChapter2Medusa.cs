using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffectForChapter2Medusa : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.5f; // How far up and down to move
    [SerializeField] private float floatSpeed = 1f; // How fast to move
    public RockThatMovesSound rockThatMovesSound;
    public AudioSource audioSourceTurnOnNotRightAtStart;
    public bool hasPlayedSound = false, playUp = true;

    private Vector3 startLocalPosition;
    private Vector3 localUpDirection;
    private float initialYOffset;

    private void Start()
    {
        startLocalPosition = transform.localPosition;

      
        if (transform.parent != null)
        {
       
            localUpDirection = transform.parent.InverseTransformDirection(Vector3.up);
        }
        else
        {
            localUpDirection = Vector3.up;
        }
        initialYOffset = Random.Range(0f, 2f * Mathf.PI);
        if(audioSourceTurnOnNotRightAtStart != null)
        {
            audioSourceTurnOnNotRightAtStart.enabled = false;
            Invoke(nameof(TurnOnAudioSource), Random.Range(0f,1f));
        }
    }
    void TurnOnAudioSource()
    {
        if (audioSourceTurnOnNotRightAtStart != null)
        {
            audioSourceTurnOnNotRightAtStart.enabled = true;
        }
    }
    private void Update()
    {

        float offsetY = Mathf.Sin(Time.time * floatSpeed + initialYOffset) * floatAmplitude;
        if (-0.01f <= offsetY && offsetY <= 0 && rockThatMovesSound != null)
        { 
           
            if (!hasPlayedSound) {
                if (playUp == false)
                {
                    playUp = true;
                    hasPlayedSound = true;
                }
                else
                {
                    rockThatMovesSound.PlayMoving();
                    hasPlayedSound = true;
                    playUp = false;
                }
                   

            }
        }
        else
        {
                       hasPlayedSound = false;

        }
        transform.localPosition = startLocalPosition + (localUpDirection * offsetY);
    }
}
