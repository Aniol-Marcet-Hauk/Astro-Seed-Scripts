using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3XP : MonoBehaviour
{
    public Vector3 endPos;
    public float maxDistRandOut = 1f;
    public eyeBoss eyeBoss;
    private void Start()
    {
        AttractionXP(); 
    }

    void AttractionXP(){
        Vector3 initialRandPosOut = transform.position + new Vector3 (UnityEngine.Random.Range(-maxDistRandOut, maxDistRandOut), UnityEngine.Random.Range(-maxDistRandOut, maxDistRandOut), 0);

        transform.DOMove(initialRandPosOut, UnityEngine.Random.Range(0.5f, 1f)).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            transform.DOMove(endPos, UnityEngine.Random.Range(0.5f, 1f)).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                eyeBoss.OnXpDeath?.Invoke();
                Destroy(gameObject);
            });
        });
    }
}
