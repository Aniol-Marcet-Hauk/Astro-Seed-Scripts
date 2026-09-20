using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidKill : MonoBehaviour
{
    [SerializeField] private Vector3 gravityAppear;
    [Space]
    [SerializeField] private Transform exitPos;
    [SerializeField] private Animator exitPosAn;

    [Space]
    [SerializeField] private GWormRotatation[] rotations;
    [SerializeField] private Transform[] resetObjects;
    private Vector3[] resetObjectPos;
    public float addDelay = 0;
    private bool respawning;

    private void Start()
    {
        resetObjectPos = new Vector3[resetObjects.Length];
        for (int i = 0; i < resetObjects.Length; i++)
        {
            resetObjectPos[i] = resetObjects[i].position;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       //disappear animation player fade black
       StartCoroutine(Spawn());
            //appear animation
        


    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(addDelay);
        //THink about thisssss Realllyyy
        Transform player = GameManager.gameManager.player;
        GameManager.gameManager.playerNOTBODY.gameObject.SetActive(false);
        //PLAY DEATH ANIMATION I GUESS
        GameManager.gameManager.playerNOTBODY.position += (exitPos.position - player.position);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //maybe add force?
        if (gravityAppear.magnitude > 0)
            Physics2D.gravity = gravityAppear;
        else
            Physics2D.gravity = exitPos.up * 9.81f;
        //camera fade black
        
        yield return new WaitForSeconds(.5f);
        foreach (GWormRotatation item in rotations)
        {
            item.Reseter();
        }
        for(int i = 0; i < resetObjects.Length; i++)
        {
            resetObjects[i].position = resetObjectPos[i];
            resetObjects[i].gameObject.SetActive(true);
        }
        //move the camera first?
        
        
        //camera unfadeblack
        yield return new WaitForSeconds(1f);
        exitPosAn.Play("PlayerExitSortidorMoc");
        yield return new WaitForSeconds(0.1f);
        GameManager.gameManager.playerNOTBODY.gameObject.SetActive(true);

    }

    public void turnOff()
    {
        gameObject.SetActive(false);
    }
}
