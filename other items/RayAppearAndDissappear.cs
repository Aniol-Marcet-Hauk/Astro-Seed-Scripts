using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayAppearAndDissappear : MonoBehaviour
{
    public float TimeStart;
    public float Times;
    private Collider2D coll;
    public GameObject sr;
    public float A;
    private void Start()
    {
        StartCoroutine("app");
        coll = GetComponent<Collider2D>();
      
    }
    IEnumerator app()
    {
        yield return new WaitForSeconds(TimeStart);
        while (true)
        {
            coll.enabled = false;
            sr.SetActive(false);
            yield return new WaitForSeconds(Times);
            coll.enabled = true;
            sr.SetActive(true);
            yield return new WaitForSeconds(Times + 0.25f);

        }

        //-(A-1)*0.3f
    }
}
