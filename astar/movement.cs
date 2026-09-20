using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class movement : MonoBehaviour
{
    //public Grid gridForPath;
    public bool walkingToPlace = true;
    private bool isWalking = false;
    public AIPath ai;
    public float distance = 5f;
    private Transform player;
    public float closeSpeed, closeAcc;
    public int tentaclesNotConnected = 0;
    public float tentacleNotCOnnectedLowerSpeed = 0.08f;

    //public Rigidbody2D rb;
    //private Vector3 firstvector;

    //public float Speed = 5f;
    //private float timerot = 0.1f;
    private void Start()
    {
        player = GameManager.gameManager.player;
    }

    private void Update()
    {
        if((player.position-transform.position).magnitude < distance)
        {
            ai.maxSpeed = closeSpeed -(tentaclesNotConnected*tentacleNotCOnnectedLowerSpeed);
            ai.maxAcceleration = closeAcc;
        }
        else
        {
            ai.maxSpeed = closeSpeed*3;
            ai.maxAcceleration = closeAcc*3;
        }

        if (walkingToPlace == true)
        {
            ai.canMove = true;
        }
        else if (walkingToPlace == false)
        {
            ai.canMove = false;
            isWalking = false;
        }    

    }
 
    
}
