using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faller : MonoBehaviour
{
   /* //make sure it is always the same
    public float timeForFall;
    public float downSpeed;
    private Vector3 vec;
    public float whenHitHowMuchTime;
    private bool reset;
    private void Start()
    {
        reset = false;
        vec = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            StartCoroutine("fall");
        }
        if(collision.collider.tag != "Player")
        {
            StartCoroutine("Destroy&putBack");
        }
    }
    
    IEnumerator fall()
    {
        yield return new WaitForSeconds(timeForFall);
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
       
        
       
    }
    IEnumerator DestroyAndputBack()
    {
        yield return new WaitForSeconds(whenHitHowMuchTime);
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.position = vec;
        rb.bodyType = RigidbodyType2D.Static;
    }*/
}
