using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxEffect : MonoBehaviour
{
    private float startposX,startposY;
    public GameObject cam;
    public float ParallaxEffect;
    
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distX = (cam.transform.position.x * ParallaxEffect);
        float distY = (cam.transform.position.y * ParallaxEffect);

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);
    }
}
