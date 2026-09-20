using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyposition : MonoBehaviour
{
    [SerializeField] private Transform copyPos;
    [SerializeField] private Vector3 center;

    // Update is called once per frame
    void Update()
    {
        transform.position = copyPos.position + center;
    }
}
