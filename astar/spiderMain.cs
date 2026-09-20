using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderMain : MonoBehaviour
{
    
    public movement move;
    public Transform header;
    public Transform trans;
    public Transform target;
    public bool walkingToPlace = true;
    private bool Attack = true;
    public float dis;

    public LayerMask LM;
    [Space]
    public float DistanceOfABall;
    public GameObject elec;
    public float velocityOfBall;
    [Space]
    public Collider2D boostElectrectute;
    public float DistanceOfBoost;
    public float velocityOfBoost;
    private Rigidbody2D rb;
    public float moveBackwords;
    public float speedBack;
    public float speedUpOfBoost, backstepDuration, maxDashDuration =1f;

    public bool hitBoost = false;
    private float velocityOfBoost2;
    public float n;
    private int nElec;
    private int nBoost;
    [Space]
    public Animator an,teethMove;

    public Transform transAn;


    public float beforeWToPlace = 3f;
    [Space]
    [SerializeField] private float timeRotation;
    private bool start = false;
    [Space]
    public float moveButDontAttack = 5f;
   
    void Start()
    {
       
        Attack = false;
        rb = transform.GetComponent<Rigidbody2D>();
        velocityOfBoost2 = velocityOfBoost;
        Invoke(nameof(WaitToStartBeforeAttack), 7f);
        move.walkingToPlace = true;
        boostElectrectute.enabled = false;

    }
    private void WaitToStartBeforeAttack()
    {
        Attack = true;
        start = true;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.tag == "ground")
        {

            hitBoost = true;

        }
    }

    void Update()
    {
        
        move.walkingToPlace = walkingToPlace;
        
        if (walkingToPlace == true)
        {
        
            transform.position = trans.position;
  

               
            Look(transform, timeRotation);
            if (hitBoost == true)
            {
                hitBoost = false;
            }
        }
        if (Attack == true)
        {
            IfAttacks();
        }
        
    }
    private void Look(Transform tr, float timerotation)
    {
        Vector3 diff = ((target.position - tr.position) * 2f).normalized;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Vector3 dif = new Vector3(0, 0, rot_z);
        Quaternion rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        tr.rotation = Quaternion.Slerp(tr.rotation, rotation, timerotation * Time.deltaTime);
    }

    void IfAttacks()
    {

        RaycastHit2D checkhit = Physics2D.Raycast(header.position, transform.up, dis, ~LM);
        if (checkhit)
        {
          
            if (checkhit.transform.tag == "Player" ||checkhit.transform.GetComponent<holdaball>()!=null)
            {
                
                n = Random.Range(0, 2);
                move.ai.enabled = false;
                if (n < 0.5)
                {
                    if(nBoost < 2)
                    {
                        StartCoroutine("boostAttack");
                        nBoost += 1;
                        nElec = 0;
                    }
                    else
                    {
                        StartCoroutine("electircAttack");
                        nElec += 1;
                        nBoost = 0;
                    }
                }
                else
                {
                    if(nElec < 2)
                    {
                        StartCoroutine("electircAttack");
                        nElec += 1;
                        nBoost = 0;
                    }
                    else
                    {
                        StartCoroutine("boostAttack");
                        nBoost += 1;
                        nElec = 0;
                    }
                    
                }
            }
        }
        

    
    }

    IEnumerator boostAttack()
    {
        Attack = false;
        walkingToPlace = false;

        rb.velocity = Vector2.zero;
        hitBoost = false;
        teethMove.SetBool("Charge", true);
        yield return new WaitForSeconds(0.3f);
        teethMove.SetBool("Charge", false);
        // Backstep (stable: fixed duration, fixed speed computed from distance)
        Vector2 backDir = -(Vector2)transform.up;
        float backSpeed = (backstepDuration <= 0f) ? 0f : (moveBackwords / backstepDuration);
        float backTime = 0f;

        while (backTime < backstepDuration)
        {
            rb.velocity = backDir * backSpeed;
            backTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.5f);

        // Dash until hitBoost OR max duration (optionally also limited by DistanceOfBoost)
        boostElectrectute.enabled = true;

        Vector2 dashDir = (Vector2)transform.up;
        float dashSpeed = velocityOfBoost2;
        float dashTime = 0f;
        float dashedDistance = 0f;

        while (!hitBoost && dashTime < maxDashDuration && dashedDistance < DistanceOfBoost)
        {
            dashSpeed += speedUpOfBoost * Time.fixedDeltaTime;
            rb.velocity = dashDir * dashSpeed;

            dashedDistance += dashSpeed * Time.fixedDeltaTime;
            dashTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        rb.velocity = Vector2.zero;
       
        hitBoost = false;

        // Resume pathing
        move.ai.enabled = false;
        yield return new WaitForSeconds(beforeWToPlace);
        boostElectrectute.enabled = false;
        walkingToPlace = true;
        trans.position = transform.position;
        move.ai.enabled = true;
        yield return new WaitForSeconds(moveButDontAttack+Random.Range(0f,1f));

        Attack = true;

    }
    
    IEnumerator electircAttack()
    {
        Attack = false;
       
        walkingToPlace = false;
        Vector3 transPos = transform.position;
        transform.position = transAn.position;
        transAn.position = transPos;
        an.SetBool("isElec", true);
        teethMove.SetBool("ElecRotate", true);
        yield return new WaitForSeconds(0.2f);
        an.SetBool("isElec", false);
        teethMove.SetBool("ElecRotate", false);
        yield return new WaitForSeconds(1f);
        
        for (int x = 0; x < 3;x++)
        {
            Vector3 a = transform.position;
            
            GameObject clone = Instantiate(elec, a,transform.rotation);
            clone.SetActive(true);
            StartCoroutine(throwElec(clone,velocityOfBall));
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(beforeWToPlace);
        trans.position = transform.position;
        walkingToPlace = true;
        move.ai.enabled = true;
        yield return new WaitForSeconds(moveButDontAttack + Random.Range(0f, 1f));
        Attack = true;


    }

    IEnumerator throwElec(GameObject elec,float velocityOfball)
    {
        Rigidbody2D elecRB = elec.GetComponent<Rigidbody2D>();
        Vector3 transUp = transform.up;
        float t = 0;
        while (t <= 1)
        {
            
            velocityOfball += Time.fixedDeltaTime *1.2f;
            t += Time.fixedDeltaTime / velocityOfball;

            elecRB.MovePosition(Vector3.Lerp(transform.position, transUp * DistanceOfABall + transform.position, t));
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1.8f);

        Destroy(elec);
    }
    
    


}