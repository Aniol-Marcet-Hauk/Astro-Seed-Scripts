using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class muscleAppears : MonoBehaviour
{
    [SerializeField] private Vector2 xy;

    [SerializeField] private float position,timeNeeded =2f,maxDistance = 4f;//maxDistance and timeneeded for muscle closes should be basically the same

    private bool move = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }
        if(collision.CompareTag("Player") && move == false)
        {
            move = true;
            Move();
        }
    }


    private void Move()
    {

        Transform player = GameManager.gameManager.player;
        Vector3 playPos = player.position;
        Vector3 addPos = new Vector3(xy.x * (position - player.position.x), xy.y * (position - player.position.y));


        player.DOMove(playPos + addPos, timeNeeded*(addPos.magnitude/maxDistance)).OnComplete(() => { move = false; }); 

        

       

    }
}
