using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider4rotatewithastar : MonoBehaviour
{


    private Rigidbody2D rb;
    [SerializeField] private Animator an;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude>0.5f)
        {
            an.SetBool("isMoving", true);
            //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            Vector3 direction = rb.velocity.normalized;
            Vector3 lerpedUp = Vector3.Lerp(transform.up, direction, Time.deltaTime * 10f);
            transform.up = lerpedUp;
        }
        else if(rb.velocity.magnitude<0.01f)
        {
            an.SetBool("isMoving", false);
        }
        else
        {
            an.SetBool("isMoving", true);
        }
    }
}
