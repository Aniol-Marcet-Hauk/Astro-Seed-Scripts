using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToungueBoss : MonoBehaviour
{
    private Animator an;
    private float speed = 1f;
    public float speedChangeRate;
    public float speedChangeVariationNeg, speedChangeVariation;
    void Start()
    {
        an = GetComponentInParent<Animator>();
        StartCoroutine(SpeedChange());
    }

     IEnumerator SpeedChange()
    {
        yield return new WaitForSeconds(speedChangeRate);
        float s = Random.Range(-speedChangeVariationNeg, speedChangeVariation);
        an.SetFloat("Speed", speed+s);
        StartCoroutine(SpeedChange());
    }
    void Update()
    {
       
    }
}
