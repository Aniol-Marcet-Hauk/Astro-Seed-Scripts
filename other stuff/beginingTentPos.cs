using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beginingTentPos : MonoBehaviour
{
    public Transform bigginingOfTent;

    private void Update()
    {
        bigginingOfTent.position = transform.position;
        bigginingOfTent.rotation = transform.rotation;
    }
}
