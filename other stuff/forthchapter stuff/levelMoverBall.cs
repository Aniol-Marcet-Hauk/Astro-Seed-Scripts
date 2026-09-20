using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelMoverBall : MonoBehaviour
{
    private holdaball hold;
    private Rigidbody2D rb;
    private DistanceJoint2D dj2D;
    private TargetJoint2D tj2D;
    [SerializeField] private ifElecCollide body;
    [SerializeField] private moveLeftHand ml;
    private bool leftHolding, rightHolding;
    public bool isItem;
    [Space]
    [SerializeField] private GameObject closeZ, openZ;
    [SerializeField] private bool hasSpawner;
    private BreakWhenPulling _breakWP;
    private void Awake()
    {
        dj2D = gameObject.GetComponent<DistanceJoint2D>();
        tj2D = gameObject.GetComponent<TargetJoint2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        _breakWP = transform.GetComponent<BreakWhenPulling>()               ;
        isItem = true;
        leftHolding = false;
        rightHolding = false;
    }
    private void Update()
    {
        if (hasSpawner && _breakWP.isBroken)
        {
            closeZ.SetActive(false);
            openZ.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        hold = collision.GetComponent<holdaball>();
        if(hold != null )
        {


            

            if (ml.ArmCounter == 1 && hold.blueBreakyThing == true)
            {
                
                if (body.GetIsGrounded() || hold.right == true && ml.holding == true || hold.right == false && ml.mr.holding == true)
                {
                    
                    if (isItem == false)
                    {
                        hold.h = false;

                        IsItem();
                    }

                }
                else if (isItem == true)
                {
                    hold.h = false;
                    IsString();
   
                }
            }
             
            else if(ml.ArmCounter == 0 && isItem == false)
            {
                IsItem();
            }
            else if(ml.ArmCounter == 2  )
            {
                if(isItem == false && body.GetIsGrounded())
                {
                    hold.h = false;
                    IsItem();
                }
                else if(isItem == true)
                {
                    hold.h = false;
                    IsString();
                }
            }
            
        }
    }
    
    void IsItem()
    {
        
        isItem = true;
        gameObject.tag = "items";
        rb.gravityScale = 0f;
        rb.mass = 0f;
        dj2D.enabled = false;
        tj2D.enabled = true;

    }

    void IsString()
    {
        
        isItem = false;
        gameObject.tag = "holdable";
        rb.gravityScale = 30f;
        rb.mass = 30f;
        dj2D.enabled = true;      
        tj2D.enabled = false;
    }

}