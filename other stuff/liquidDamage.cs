using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class liquidDamage : MonoBehaviour
{

    //the thing is, this is completeley unnecessary
    /*
    public float timeForFall;
    private bool falling = true;
    public Transform positionPlayerFalls;
    public float fallX, fallY;



    //IMPORTANTISSIM CANVIAR ELECTRICITY PQ ESTIGUI DINS D'UN SCRIPT DEL JUGADOR
    private void Start()
    {

       
        if(timeForFall != 0)
        {
            StartCoroutine(TimeFalling());
        }
        else
        {
            falling = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (falling == true)
            {
                StartCoroutine(Fall());
            }
            else
            {

            }
           
        }
    }
   
    
    IEnumerator TimeFalling()
    {
        yield return new WaitForSeconds(timeForFall);
        falling = false;
    }
    IEnumerator Fall()
    {
        
        Transform player = FindObjectOfType<ifElecCollide>().transform;
        Vector3 playPos = player.position;
        float cosX = Vector3.Dot(positionPlayerFalls.position - playPos, positionPlayerFalls.right);

        player.DOMove(playPos + cosX * positionPlayerFalls.right, fallX);

        yield return new WaitForSeconds(fallX);
        player.DOMove(positionPlayerFalls.position, fallY);
        yield return new WaitForSeconds(fallY+1f);

    }*/

    
}
