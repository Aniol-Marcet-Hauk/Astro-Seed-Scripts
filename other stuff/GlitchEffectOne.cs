using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffectOne : MonoBehaviour
{
    [SerializeField] private Transform white, green, red;
    [SerializeField] private float minTime = 5, maxTime = 10, maxMovement,timeShake,timeShakeMax = .15f;
    [Space]
    [SerializeField] private SpriteRenderer inv;
    private Vector3 originalPos;
    private float shakeTimer;
    [Space]
    [SerializeField] private bool appearBoss = false;
    [SerializeField] private SpriteRenderer sr;
    private void Start()
    {
        originalPos = white.localPosition;
        shakeTimer = Random.Range(timeShake, timeShakeMax);
        if(appearBoss == true)
        {
            sr.enabled = false;
            white.gameObject.SetActive(false);
            green.gameObject.SetActive(false);
            red.gameObject.SetActive(false);
        }
        StartCoroutine(GlitchEffect1());
    }

    private IEnumerator GlitchEffect1()
    {
        
        
        yield return new WaitForSeconds(Random.Range(minTime,maxTime));
        if (inv != null)
        {
            inv.enabled = false;
        }
        if (appearBoss == true)
        {
            sr.enabled = true;
            white.gameObject.SetActive(true);
            green.gameObject.SetActive(true);
            red.gameObject.SetActive(true);
        }
        while (shakeTimer > 0)
        {
            white.localPosition = originalPos + Random.insideUnitSphere* maxMovement;
            green.localPosition = originalPos + Random.insideUnitSphere * maxMovement;
            red.localPosition = originalPos + Random.insideUnitSphere * maxMovement;
            yield return new WaitForFixedUpdate();
            shakeTimer -= Time.fixedDeltaTime; 
        }


        shakeTimer = Random.Range(timeShake, timeShakeMax);
        white.localPosition = originalPos;
        green.localPosition = originalPos;
        red.localPosition = originalPos;
        if (inv != null)
        {
            inv.enabled = true;
        }
        if (appearBoss == true)
        {
            sr.enabled = false;
            white.gameObject.SetActive(false);
            green.gameObject.SetActive(false);
            red.gameObject.SetActive(false);
        }
        StartCoroutine(GlitchEffect1());
    }
}
