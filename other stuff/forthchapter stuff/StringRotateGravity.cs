using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringRotateGravity : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
        transform.up = Physics2D.gravity.normalized * -1f;
        
    }
}
