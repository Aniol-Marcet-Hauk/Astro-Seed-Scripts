using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentMovement : MonoBehaviour
{
    public Animator an; //REMEMBER TO CHANGE
    public Transform toPosition;
    public float velocity;
    public float velocityBack;
    public float secondsTillM;
    public float secondsTillMBack;
    private Vector3 endPos;
    private Rigidbody2D rb;
    private Vector3 startPosition;
    private float t,t1;

    private float r;
    public bool consistent;
    public bool Ifholding;
    private bool Move;
    private bool dir;
    [Space]
    public bool fadeInOut = false;
    [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        endPos = toPosition.position;
       

        startPosition = transform.position;
        if(consistent == true)
        {
            StartCoroutine("move");
        }
        else
        {
            StartCoroutine("moveWhenHold");
        }
        Ifholding = false;
        if (an == null)
        {
            return;
        }
        an.SetFloat("Speed", 2 / velocity * 2);
        r = t * -2;
        an.SetFloat("speedBack", 2 / velocityBack * 2);


    }
    IEnumerator move()
    {
        
        while (true)
        {
            while (t1 < 1)
            {
                Move = true;
                //an.SetBool("is moving", true);
                dir = true;
                //an.SetBool("GoingInfront", true);
                t1 += Time.fixedDeltaTime / velocity;
                float clampedT = Mathf.Clamp01(t1);
                t = clampedT;
                if (fadeInOut)
                {
                    t = moveCurve.Evaluate(t);
                    //t = t1 * t1 * (3f - 2f * t1);
                }
                
                rb.MovePosition(Vector3.Lerp(startPosition, endPos, t));
                yield return new WaitForFixedUpdate();
            }

            Move = false;
            //an.SetBool("is moving", false);
            yield return new WaitForSeconds(secondsTillMBack);
            
            while (t1 > 0)
            {
                Move = true;
                //an.SetBool("is moving", true);
                dir = false;
                //an.SetBool("GoingInfront", false);
                t1 -= Time.fixedDeltaTime / velocityBack;
                float clampedT = Mathf.Clamp01(t1);
                t = clampedT;
                if (fadeInOut)
                {
                    t = moveCurve.Evaluate(t);
                }
                rb.MovePosition(Vector3.Lerp(startPosition, endPos, t));
                yield return new WaitForFixedUpdate();
            }
            Move = false;
            //an.SetBool("is moving", false);
            yield return new WaitForSeconds(secondsTillM);
            
        }
        

    }
    public IEnumerator moveWhenHold()
    {
        while (true)
        {
            while (Ifholding == true)
            {
                yield return new WaitForSeconds(secondsTillM);
                while (t1 < 1)
                {
                    Move = true;
                    dir = true;
                    t1 += Time.fixedDeltaTime / velocity;
                    float clampedT = Mathf.Clamp01(t1);
                    t = clampedT;
                    if (fadeInOut)
                    {
                        t = moveCurve.Evaluate(clampedT);
                    }
                    rb.MovePosition(Vector3.Lerp(startPosition, endPos, t));
                    yield return new WaitForFixedUpdate();
                }
                Move = false;
                yield return new WaitForSeconds(secondsTillMBack);
                while (t1 > 0)
                {
                    Move = true;
                    //an.SetBool("is moving", true);
                    dir = false;
                    t1 -= Time.fixedDeltaTime / velocityBack;
                    float clampedT = Mathf.Clamp01(t1);
                    t = clampedT;
                    if (fadeInOut)
                    {
                        t = moveCurve.Evaluate(clampedT);
                    }
                    rb.MovePosition(Vector3.Lerp(startPosition, endPos, t));
                    yield return new WaitForFixedUpdate();
                }
                Move = false;
                

               
            }
            yield return null;
        }


    }
    private void Update()
    {
        if (an == null)
        {
            return;
        }


        if (Move == true)
        {
            an.SetBool("is moving", true);
        }
        else
        {
            an.SetBool("is moving", false);
        }
        if(dir == true)
        {
            an.SetBool("GoingInfront", true);
        }
        else
        {
            an.SetBool("GoingInfront", false);
        }
     
    }

}
