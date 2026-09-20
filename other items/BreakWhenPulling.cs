using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BreakWhenPulling : MonoBehaviour
{
    [SerializeField] private bool startChecking = false;
    private bool isPulling;
    public float distance;
    
    public bool a = false;
    private Vector3 vec;
    
    
    private Animator an;
    public bool isBroken { get; private set; }
    [SerializeField] private GameObject deactivateAsWell;
    [SerializeField] private Transform startPos;
    private Collider2D col;
    private void Start()
    {
        
        
        an = gameObject.GetComponent<Animator>();
        vec = transform.position;
        if (startPos != null)
        {
            vec = startPos.position;
        }

    }
    private void OnEnable()
    {
        isBroken = false;
        a = false;
        isPulling = false;
        if (deactivateAsWell != null)
        {
            deactivateAsWell.SetActive(true);
        }
       
    
     

    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {

        
        if (collision.tag != "Player")
        {
            return;
        }
        Debug.Log("triggered");
        DoStuff();
    
    }
    void DoStuff(){
        if (a == false && startChecking == true)
        {
            if (IfIsPulling())
            {
                Debug.Log("isPulling");
                isPulling = true;
                a = true;
                StartCoroutine("breaking");

            }

        }
        if (a == true)
        {
          
            if (!IfIsPulling())
            {
                Debug.Log("isnotPpulling");
                a = false;
                isPulling = false;
            }
        }
    }
   


    IEnumerator breaking()
    {
        //an.Play("Shater");
        float t = Time.time;
        Debug.Log("yeap");
        while (isPulling == true)
        {
            
            if (IfIsPulling() == false)
            {
                isPulling = false;
                a = false;
                break;
            }
            else if (Time.time - t >= 2.5f)
            {

                //an.Play("Destroy");
                isBroken = true;
                yield return new WaitForSeconds(0.1f);
                if (deactivateAsWell != null)
                {
                    Debug.Log("wehatsdjlfasjf");
                    deactivateAsWell.SetActive(false);
                }
                gameObject.SetActive(false);

            }

            yield return new WaitForFixedUpdate();

        }
    }


    private bool IfIsPulling()
    {
        if (startPos != null)
        {
            vec = startPos.position;
        }
        if (Mathf.Pow(vec.x - transform.position.x, 2) >= distance || Mathf.Pow(vec.y - transform.position.y, 2) >= distance)
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }
    public void StartCheckingV(bool startChecking)
    {
        this.startChecking = startChecking;
            
    }
}
