using DG.Tweening;
using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tinyEye : MonoBehaviour
{
    [Header("Rotate")]
    public Transform player;
    
    public float timerotation = 0.1f;
    [Space]
    [Header("RayCast")]
    public Transform eyepoint;
    public float distanceRay;
    public LayerMask LM;
    [Space]
    [Header("Attack")]

   
    public GameObject Glitch;
    public bool attacking;
    private bool isAttacking;
    private bool cantAttackYet = true;
    public float speedAttack = 1f;


    public float time;

    private Animator an;

    [SerializeField] private EyeHeartSystem eyeHSystem;
    //not a fan of this but easiest way to do it :/


    //ok adding the new way of doing it :
    [Header("Lazer")]
    [SerializeField] private GameObject Electric;
    [SerializeField] private Transform front;
    private GameObject Electr;
    private electricity el;
    private LineRenderer lr;

    private bool hasEye = false,end = false;
    private RaycastHit2D ray;
    [Space]
    [Header("WierdThingsForEndOfFase")]
    [SerializeField] private Transform posEndPlayer;
    [SerializeField] private Animator doorEnd;
    public float jumpPower,jumpDuration;
    void Start()
    {
        Electr = Instantiate(Electric);
        fadeOutLazer();
        lr = Electr.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        el = gameObject.GetComponent<electricity>();
        an = gameObject.GetComponent<Animator>();
        an.SetFloat("SpeedAttack", speedAttack);
        if (eyeHSystem != null)
        {
            hasEye = true;
        }
        cantAttackYet = true;
        Invoke(nameof(CanAttack), Random.Range(0.5f, 4f));
        
    }
    private void CanAttack()
    {
        cantAttackYet = false;
    }
    void Update()
    {
        if(end == true || cantAttackYet)
        {
            return;
        }
        if (hasEye)     //this should 100% be in a seperate script ajajajajjaja :) defenetly not having a fucking panic attack or anything. defenetly don't want to die right now :)
        {
            if(eyeHSystem.isActiveAndEnabled == false )
            {
  
                an.SetBool("Rest", true);
                an.Play("EyeBreak");
                doorEnd.Play("OpenFInalchptr3");
                end = true;
                Rigidbody2D rb = GameManager.gameManager.player.GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                
                rb.transform.DORotate(Vector3.zero, 2f);
                rb.transform.DOMove(rb.transform.position + new Vector3(0, 1, 0), 2f);
                Invoke(nameof(DoWierdAnimation), 2.4f);
                
                //ANIMATION EYEBREAK AND OPEN DOOR
                return;
            }

            if (eyeHSystem.boolBreak ==true)
            {
                an.SetBool("Rest", true);

                return;
            }
            an.SetBool("Rest", false);
            
        }
        //COMPLETELY CHANGE
        ray = Physics2D.Raycast(eyepoint.position, transform.up, distanceRay, ~LM);



        lr.SetPosition(0, front.position);
        if (ray)
        {
            lr.SetPosition(1, ray.point);

            if ((ray.transform.tag == "Player"||ray.transform.GetComponent<holdaball>()) && attacking == false)
            {

                
                an.SetBool("Attacking", true);
              //  StartCoroutine(Attack());
                        
                    
            }
            else
            {
                
                time = 0;
            }
           
        }
        else
        {
            lr.SetPosition(1, front.position+front.up*distanceRay);
        }
        if (isAttacking == true)
        {
            return;

        }
        Look();

    }
    private void Look()
    {
        Vector3 diff = ((player.position - transform.position) * 2f).normalized;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Vector3 dif = new Vector3(0, 0, rot_z);
        Quaternion rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, timerotation);
    }
    /*IEnumerator Attack()
    {
        
        attacking = true;
        yield return new WaitForSeconds(timeAttacks);
        //this is all just animation based
        //play animation the eye makes
        //yield return new WaitForSeconds(Wtvr time the animation takes)
        GameObject clone = Instantiate(Glitch, transform.position,transform.rotation);
        clone.SetActive(true);
        
        yield return new WaitForSeconds(0.20f);
        time = 0;
        Destroy(clone);
        an.SetBool("Attacking", false);
        attacking = false;
        
    }*/
    public void GlitchEffectEnding()
    {
        StartCoroutine(GlitchEf());
    }
    IEnumerator GlitchEf()
    {

        AnalogGlitch anGlitch = FindObjectOfType<AnalogGlitch>();
        anGlitch.enabled = true;
        //0.97/ 0.27/ 0.67/ 1
        float t = 0f;
        while(t< 1f)
        {
            t += Time.fixedDeltaTime;
            anGlitch.scanLineJitter = t * .97f;
            anGlitch.verticalJump = t * .3f;
            anGlitch.horizontalShake = t * .67f;
            anGlitch.colorDrift = 1f;

            yield return new WaitForFixedUpdate();
        }

    }

    private void DoWierdAnimation()
    {
        Transform player = GameManager.gameManager.player;
        player.DORotate( new Vector3(0, 0, 1000), jumpDuration, RotateMode.FastBeyond360).SetEase(Ease.OutBounce);
        player.DOJump(posEndPlayer.position, jumpPower, 1, jumpDuration).SetEase(Ease.OutBounce);
    }
    //for the final tiny eye LOL
    public void LoadSceneBoss()
    {
        GameManager.gameManager.LoadNextScene();
    }

    public void IsAttackingTrue() { 
        isAttacking = true; 
        if (hasEye)
        {
            eyeHSystem.LowerEnB(1f);
        }
    }
    public void IsAttackingFalse() { isAttacking = false; attacking = false; an.SetBool("Attacking", false); }

    public void CheckHit()
    {
        if(ray && ray.transform.tag == "Player")
        {
            Debug.Log("HIT PLAYER");
            el.Elec();
            if(eyeHSystem != null)
            {
                eyeHSystem.AddEnB(1.5f);
            }
        }
       
    }
    public void fadeInLazer()
    {
        Electr.SetActive(true);
        
        Color startColor = new Color(113f / 255f, 255f / 255f, 25f / 255f, 0f);
        Color endColor = new Color(113f / 255f, 255f / 255f, 25f / 255f, 50f / 255f);
        lr.DOColor(new Color2(startColor, startColor), new Color2(endColor, endColor), 1f);
    }

    public void ActivateLazer()
    {
        Color startColor = new Color(113f / 255f, 255f / 255f, 25f / 255f, 50f / 255f);
        Color endColor = new Color(113f / 255f, 255f / 255f, 25f / 255f, 1f);
        lr.DOColor(new Color2(startColor, startColor), new Color2(endColor, endColor), 0.1f);
        StartCoroutine(GlitchyWierd());

        

    }
    private IEnumerator GlitchyWierd()
    {
        /*GameObject clone = Instantiate(Glitch, transform.position, transform.rotation);
      clone.SetActive(true);
      Vector2 frontPos = front.position;  
      clone.transform.position = frontPos;
      float speedGlitch = Vector2.SqrMagnitude(ray.point - frontPos) *.002f;
      clone.transform.DOMove(ray.point, speedGlitch ).SetEase(Ease.Unset);
      //gotta check if change magic number lol
      yield return new WaitForSeconds(speedGlitch);
      Destroy(clone); */
        yield return null;
    }

    public void fadeOutLazer()
    {
        Color startColor = new Color(113f / 255f, 255f / 255f, 25f / 255f, 50f/255f);
        Color endColor = new Color(113f / 255f, 255f / 255f, 25f / 255f, 0f);
        lr.DOColor(new Color2(startColor, startColor), new Color2(endColor, endColor), 0.1f);
        Electr.SetActive(false);
    }

}
