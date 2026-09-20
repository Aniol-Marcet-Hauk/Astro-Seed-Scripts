using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeRigidbody : MonoBehaviour
{
    public float time;
    private float t;
    private Rigidbody2D rb;
    private Collider2D coll;
    public bool andCollider;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
          if(collision.collider.tag == "ground")
        {
            Destroy(rb);
            if (andCollider)
            {
                Destroy(coll);
            }
            Destroy(this);
        }
    }
    void Start()
    {
        rb = transform.GetComponent< Rigidbody2D>();
        coll = transform.GetComponent<Collider2D>();
        
    }
    
    
      
            
        
   
    
}
