using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteMaskRadar : MonoBehaviour
{
    private float t;
    public float velocity;
    public float timebetweenResize;
    public float timebetweenDarkness;
    public float startSize;
    public float endSize;
    private Vector3 VstartSize;
    private Vector3 VendSize;
    public Animator an;
    private void Start()
    {
        VstartSize = new Vector3(startSize, startSize, 1);

        VendSize = new Vector3(endSize, endSize, 1);
        StartCoroutine("now");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator now()
    {
        while (true)
        {
            while (t < 1)
            {
               
                t += Time.fixedDeltaTime / velocity;
                transform.localScale = Vector3.Lerp(VstartSize, VendSize, t);
 
                yield return new WaitForFixedUpdate();
            }

            
            yield return new WaitForSeconds(timebetweenDarkness);
            an.SetBool("Now", false);
            yield return new WaitForSeconds(1.35f);
            an.SetBool("Now", true);
            t = 0;
            transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(timebetweenResize);

        }

    }
}
