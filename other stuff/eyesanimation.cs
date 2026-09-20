using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyesanimation : MonoBehaviour
{
    public float maxTimeRand = 30, minTimeRand = 10;
    public string[] animNames;

    public Animator anim;
    private bool on = true;
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RandomAnim());
    }
    private void OnDisable()
    {
        on = false;
    }
    private void OnEnable()
    {
        on = true;
        StartCoroutine(RandomAnim());
    }
    IEnumerator RandomAnim()
    {
        while (on)
        {
            yield return new WaitForSeconds(Random.Range(minTimeRand, maxTimeRand));
            anim.Play(animNames[Random.Range(0, animNames.Length)]);
        }
    }
}
