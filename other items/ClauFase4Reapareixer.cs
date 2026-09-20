using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClauFase4Reapareixer : MonoBehaviour
{
    public Transform startPos;
    private Vector3 vec;
    void Start()
    {
        
        if (startPos != null)
        {
            vec = startPos.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = vec;
    }

}
