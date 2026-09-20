using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLiquidDirection : MonoBehaviour
{
    public Material mat;

    void Start()
    {
        InvokeRepeating("ChangeShader", 0.1f, 0.2f);
    }

    void ChangeShader()
    {
        mat.SetVector("_gravityMultip", Physics2D.gravity * 0.1019f);
        
    }
    
    
}
