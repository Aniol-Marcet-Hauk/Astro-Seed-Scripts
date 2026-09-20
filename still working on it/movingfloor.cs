using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingfloor : MonoBehaviour
{

    private Vector3 x;
    void Start()
    {
        x = Vector3.right;
        StartCoroutine("floor");
       
    }
    IEnumerator floor()
    {
        while (true)
        {
            transform.Translate(transform.position + x);
            yield return new WaitForSeconds(3);
            x *= -1;
        }
        

        
    }

}
