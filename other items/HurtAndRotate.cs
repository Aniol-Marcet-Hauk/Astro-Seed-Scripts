using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAndRotate : MonoBehaviour
{
    public string Rotate;

   
    [SerializeField]private Animator an;
    private Rigidbody2D rb;
    private TargetJoint2D targetJoint;
    public float pos;
    public float dirX = 1;

    public Vector3 startPos = new Vector3(0, -9.81f, 0);
    public Vector3 endPos = new Vector3(0, 9.81f, 0);

    private void Start()
    {
        an.speed = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
        targetJoint = gameObject.GetComponent<TargetJoint2D>();

    }

    private void Update()
    {
        if(GameManager.gameManager.gravityIsChanging){
            return;
        }
        pos =   Mathf.Abs( transform.position.x- targetJoint.target.x)+ Mathf.Abs(transform.position.y - targetJoint.target.y);
        
        pos= Mathf.Clamp01(pos / 1.6f); 
        // 
        Physics2D.gravity = Vector3.Slerp(startPos, endPos, pos);

        if (an != null)
        {
            float tclamp = pos< 1 ? pos : .99f;
            an.SetFloat("motion", tclamp);
        }




        //anim.time = pos - 0.5f;

    }
    /*private void FixedUpdate()
    {
        if (pos > 1.55)
        {
       
        }
    } */




}
