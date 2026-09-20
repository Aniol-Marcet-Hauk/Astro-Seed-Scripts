using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAn : MonoBehaviour
{
    public float time = 2.3f;
   

    public IEnumerator Demolish()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
