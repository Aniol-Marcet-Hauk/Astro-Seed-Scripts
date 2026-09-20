using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despreniment : MonoBehaviour
{
    public Animator an;
    public string animationName;
    [Space]
    public float minTimeBetween, maxTimeBetween;
    private bool wait;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(wait == false && collision.GetComponent<ifElecCollide>()!= null )
        {
            wait = true;
            an.Play(animationName);
            StartCoroutine(WaitCo());
        }
    }
    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBetween, maxTimeBetween));
        wait = false;
    }
}
