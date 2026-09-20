using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onetriggerisequal : MonoBehaviour
{
    public Collider2D coll2;

    

    // Update is called once per frame
    void FixedUpdate()
    {
        coll2.isTrigger = transform.GetComponent<Collider2D>().isTrigger;
    }
}
