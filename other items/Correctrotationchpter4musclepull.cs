using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correctrotationchpter4musclepull : MonoBehaviour
{
    public Transform pos;
    Vector3 vec;

    public float addToRot;
    private void Start()
    {
        vec = pos.position;
    }

    void Update()
    {
        Look();
    }
    private void Look()
    {
        Vector3 diff = ((vec - transform.position) * 2f).normalized;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90+addToRot);

    }
}
