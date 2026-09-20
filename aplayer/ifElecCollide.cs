using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ifElecCollide : MonoBehaviour
{
    public holdaball hold;
    public holdaball hold2;
    private bool isElec = false;
    private bool isGrounded;
    public bool isConnectedToBlue;
    private bool movingWhenever = false;

    private Rigidbody2D rb;
    private void Start()
    {
        GameManager.gameManager.player = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground" )
        {
            isGrounded = true;
            if(isElec == true)
            {
                hold.electrecuted = false;
                hold2.electrecuted = false;
                isElec = false;
            }

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGrounded = false;
        }
    }

    private void Update()
    {
        if (hold.electrecuted == true && isElec == false)
        {
            StartCoroutine("elec");
        }

#if UNITY_EDITOR
        if(InputManager.instance.moveWhenever == true)
        {
           movingWhenever =true;
           rb.isKinematic = true;
           rb.position+=InputManager.instance.leftHand * Time.deltaTime * 10f;
        }
        else if(movingWhenever == true) {
        
            movingWhenever=false;
            rb.isKinematic = false;
        }
#endif
    }
    
    IEnumerator elec()
    {
        yield return new WaitForSeconds(0.9f);
        if (hold.electrecuted == true)
        {
            isElec = true;
        }
        
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

}
