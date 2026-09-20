using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWormRotatation : MonoBehaviour
{
    public Vector2 startPos, endPos;
    //public Animation an;
   
    //public string anName;
    public float vel = 1f;
    public bool coll;
    [SerializeField] private int playerCount;
    public float t;
    [SerializeField] private GWormRotationBack gBack;

    [Space]
    public Animator an;

    // track the active coroutine so we can stop/replace it safely
    private Coroutine rotateCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        if (an != null)
        {
            an.SetFloat("motion", 0f);
        }
        //an[anName].speed = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         
        if (t < 1f && GameManager.gameManager.gravityIsChanging == false)
        {
            Debug.Log("Player entered");

            if (!coll)
            {
                coll = true;
                // stop existing rotate coroutine if any and start forward rotation
                if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
                rotateCoroutine = StartCoroutine(Rotate(1f, vel, true));
            }
        }
    }

   /* private void OnTriggerExit2D(Collider2D collision)
    {
        playerCount = Mathf.Max(0, playerCount - 1);

        // when the last player leaves, mark coll false and start reverse rotation back to t==0
        if (coll && playerCount == 0)
        {
            coll = false;
            if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
            rotateCoroutine = StartCoroutine(Rotate(0f, vel, false));
        }
    }  */

    private IEnumerator Rotate(float targetT, float velocity, bool forward)
    {
        // ensure velocity is sensible
        if (velocity <= 0f) velocity = 1f;

        GameManager.gameManager.gravityIsChanging = true;

       
        float sign = forward ? 1f : -1f;
        targetT = Mathf.Clamp01(targetT);

        
        while (true)
        {
            // if coll no longer matches the requested direction ( players left/entered), stop progression
            if (coll != forward)
                break;

            // move t toward target using fixed delta time (coroutine uses WaitForFixedUpdate)
            t = Mathf.Clamp01(t + sign * (Time.fixedDeltaTime / velocity));
            Physics2D.gravity = Vector3.Slerp(startPos, endPos, t);

            if (an != null)
            {
                float tclamp = t < 1 ? t : .99f;
                an.SetFloat("motion", tclamp);
            }

            // finished?
            if ((forward && t >= targetT) || (!forward && t <= targetT))
                break;

            yield return new WaitForFixedUpdate();
        }

        GameManager.gameManager.gravityIsChanging = false;

        if (an != null)
        {
            float tclamp = targetT < 1 ? targetT : .99f;
            an.SetFloat("motion", tclamp);
        }

        if (t >= 1f && gBack != null)
        {
       
            gBack.t = 1;
        }

        rotateCoroutine = null;
    }

    public void Reseter()
    {
        t = 0;
        Debug.Log("reset");
        playerCount = 0;
        if (an != null)
        {
            an.SetFloat("motion", 0.01f);
        }
        if (gBack != null)
        {
            gBack.t = 0;
        }
        // ensure gravity resets immediately
        Physics2D.gravity = Vector3.Slerp(startPos, endPos, t);
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
            rotateCoroutine = null;
        }
        coll = false;
        GameManager.gameManager.gravityIsChanging = false;
    }
    /*void FixedUpdate()
    {
        if (coll == true)
        {
            Physics2D.gravity = Vector3.Slerp(startPos, endPos, t);
            t = Time.fixedDeltaTime / vel;
            an[anName].time = t;
            an.Play(anName);
            yield return new WaitForFixedUpdate();
        }
        else if( t>)
        {
            Physics2D.gravity = Vector3.Slerp(startPos, endPos, t);
            t = Time.fixedDeltaTime / vel;
            an[anName].time = t;
            an.Play(anName);
            yield return new WaitForFixedUpdate();
        }
    }*/
}
