using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentecles : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D endPos;
    private movement move;
    [SerializeField] private Transform pYouFollow,header,startPos;
    public float dis;
    [SerializeField] private LayerMask LM;
    [SerializeField] private float velocity;
    public bool isConnected = true;
    public float dot;
    public float TT;
    [Space]
    [SerializeField] private Transform startPosOfEnd;
    [Space]
    public bool bossAlreadyStarted = false;
    [SerializeField] private movement moveOfBoss;
    private bool countedInBoss = false;

    private void Start()
    {
        move = pYouFollow.GetComponent<movement>();
        
        if(startPosOfEnd != null)
        {
            isConnected = true;
            StartPos();
        }
       


    }
    private void Update()
    {
        if(move.walkingToPlace == true && isConnected == false)
        {
            
                 
            StartCoroutine(MOVEE());


            
        }
        if(isConnected == true && countedInBoss == false)
        {
            moveOfBoss.tentaclesNotConnected--;
            countedInBoss = true;
        }
        else if(isConnected == false && countedInBoss == true)
        {
            moveOfBoss.tentaclesNotConnected++;
            countedInBoss = false;
        }
       
    }
    public void StartPos() {
        isConnected = true;

        StartCoroutine(Connect(startPosOfEnd.position));
        
  

    }


    /*private IEnumerator idkMan()
    {
        yield return new WaitForSeconds(0.5f);
        isConnected = false;
    }*/

    private IEnumerator MOVEE()
    {
        
        RaycastHit2D checkhit = Physics2D.Raycast(startPos.position, new Vector2(pYouFollow.up.x + Random.Range(-1f, 1f), pYouFollow.up.y + Random.Range(-1f, 1f)), dis, LM);
        if (checkhit && checkhit.transform.tag == "ground")
        {
            isConnected = true;
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            StartCoroutine(Connect(checkhit.point));
          
           

        }
        
        
    }
    public IEnumerator Connect(Vector3 point)
    {
        isConnected = true;


        float t = 0;
        endPos.bodyType = RigidbodyType2D.Kinematic;
        while (t < 0.3f)
        {

            yield return new WaitForFixedUpdate();
            t += Time.fixedDeltaTime / (velocity + Random.Range(-0.3f, 0.3f));
            endPos.MovePosition(Vector3.Lerp(endPos.position, point, t));


        }

        endPos.bodyType = RigidbodyType2D.Static;
        //move.Speed = move.Speed + 0.06f;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            Vector2 heading = new Vector2(endPos.position.x - startPos.position.x, endPos.position.y - startPos.position.y);
            dot = Vector3.Dot(heading, startPos.transform.up);

            if (dot < -0.2)
            {
                endPos.bodyType = RigidbodyType2D.Dynamic;
                isConnected = false;
                //move.Speed = move.Speed - 0.06f;
                break;
            }
        }
    }
}