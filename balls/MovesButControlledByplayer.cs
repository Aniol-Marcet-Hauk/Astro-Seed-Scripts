using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesButControlledByplayer : MonoBehaviour
{
    public Transform startPos;

    [SerializeField] private Rigidbody2D rb;
    private bool isMoving = false;
    [SerializeField] private AIPath ai;

    private void Start()
    {
        ai.isStopped = true;
    }
    void Update()
    {
        if(startPos.position != transform.position)
        {
            if(isMoving ==true || GameManager.gameManager.hingeLeft.attachedRigidbody==rb || GameManager.gameManager.hingeRight.attachedRigidbody==rb)
            {             
             
                return;

            }
            isMoving = true;
            ai.isStopped = false;

        }
        else if(isMoving == true)
        {
            Debug.Log("Moving false whatttt");
            isMoving = false;
            ai.isStopped = true;
        }
    }

 
}
