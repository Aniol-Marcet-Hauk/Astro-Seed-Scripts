using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazzierCubicInter___ : MonoBehaviour
{
    public float I;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform pointD;
    [SerializeField] private Transform pointABCD;
   
    
    void Update()
    {
        I = (I + Time.deltaTime) % 1.2f;
        pointABCD.position = CubicInter(pointA.position, pointB.position, pointC.position, pointD.position, I);
        
        if(I >= 1)
        {
            pointABCD.gameObject.SetActive(false);
        }
        if(I<1 && I > 0)
        {
            pointABCD.gameObject.SetActive(true);
        }
        
    }


    private Vector3 QuadraticInter(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, I);
    }


    private Vector3 CubicInter(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab_bc = QuadraticInter(a, b, c, t);
        Vector3 bc_cd = QuadraticInter(b, c, d, t);
        return Vector3.Lerp(ab_bc, bc_cd, I);
    }
}
