using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
 
    private float length, startPosition;


    [SerializeField] private float effectSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    
        transform.position = new Vector3(transform.position.x, transform.position.y - effectSpeed, transform.position.z);
        if (transform.position.y< startPosition - length)
        {
           
            transform.position = new Vector3(transform.position.x, startPosition, transform.position.z);
        }
       
    }
}
