using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfToFarLeave : MonoBehaviour
{
    private Transform player;
    private Animator an;
    private bool extraSpeed;
    public float speedMult, distance;
    private void Start()
    {
        player = GameManager.gameManager.player;
        extraSpeed = false;
        an = transform.parent.parent.GetComponent<Animator>();
    }
    private void Update()
    {
        if(extraSpeed == false && (player.position-transform.position).magnitude >= distance)
        {
            //try
            //{
                an.speed = speedMult;
            extraSpeed = true;
            //}
            //catch { Debug.Log("Missing animator float speed"); }

        }
        else if(extraSpeed == true && (player.position - transform.position).magnitude < distance)
        {
            //try
            //{
            an.speed = 1f;
            extraSpeed = false;
            //}
            //catch { Debug.Log("Missing animator float speed"); }
        }
    }
}
