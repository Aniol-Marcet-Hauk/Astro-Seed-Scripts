using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovableHold : MonoBehaviour
{
    //it has to start with the game  tag items too work
    // it works

    public Transform startPos;
    public GameObject endPos;
    public float tBefRespawn;
    private Rigidbody2D rb;
    private holdaball hold;
    private bool End = false;
    public bool tCheckS = false;
    private Vector3 endVec3;
    private Vector2 originalScale;

    private bool holding = false;
    private Vector3 startPosIfNULL;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        if(endPos!=null)
            endVec3 = endPos.transform.position;
        if(startPos == null)
        {
            startPosIfNULL = transform.position;
        }
       
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
     
        if (collision.GetComponent<holdaball>()!= null && End == false && tCheckS == false)
        {
            hold = collision.GetComponent<holdaball>();
            if (hold.right == false && GameManager.gameManager.hingeLeft.connectedBody == rb || hold.right == true && GameManager.gameManager.hingeRight.connectedBody == rb)
            {
                Debug.Log("holding");
                Holding();
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == endPos && End == false)
        {
            EndPos();
        }
    }


    
    void Holding()
    {
        tCheckS = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(TimeCheck());
    }

    void EndPos()
    {
        hold.h = false;
        gameObject.tag = "holdable";
        transform.DOMove(endVec3, .1f);
        transform.DORotate(Vector3.zero, .1f);
        rb.bodyType = RigidbodyType2D.Static;
        End = true;
        
    }


    IEnumerator TimeCheck()
    {
        if (tBefRespawn <= 0f)
        {
            // nothing to do
            yield break;
        }

        // Determine how many 1-second pulses we can fit into the total time.
        int pulses = Mathf.FloorToInt(tBefRespawn);
        if (pulses <= 0)
        {
            // if total < 1s, just wait the full time and then proceed
            yield return new WaitForSeconds(tBefRespawn);
        }
        else
        {
            // Spread any fractional remainder before the first pulse so total time is exactly tBefRespawn.
            float initialDelay = tBefRespawn - pulses;
            if (initialDelay > 0f)
            {
                yield return new WaitForSeconds(initialDelay);
            }

            float growMultiplier = 1.35f;
            // choose a grow duration so the full grow+shrink fits within ~1 second
            float growDuration = 0.15f; // forward duration; full yoyo = ~0.9s

            for (int i = 0; i < pulses; i++)
            {
                if (End != false)
                    yield break;

                // start the grow-then-shrink tween (Yoyo). It will complete in ~2 * growDuration.
                transform.DOScale(originalScale * growMultiplier, growDuration)
                         .SetEase(Ease.OutQuad)
                         .SetLoops(2, LoopType.Yoyo);

                // wait exactly 1 second between pulses so effect happens "after every second"
                yield return new WaitForSeconds(1f);
            }
        }

        if (End == false)
        {

            GetComponent<Collider2D>().enabled = false;
            hold.Mpurplehold = true;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            if(startPos == null)
            {
                transform.position = startPosIfNULL;
            }
            else
            {
                transform.position = startPos.position;
            }
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled = true;
            tCheckS = false;

        }
    }


    
}
