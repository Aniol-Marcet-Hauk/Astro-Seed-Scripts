using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setpositionsame : MonoBehaviour
{
    public Transform trans;

    // Update is called once per frame
    void Update()
    {
        transform.position = trans.position;
    }
}
