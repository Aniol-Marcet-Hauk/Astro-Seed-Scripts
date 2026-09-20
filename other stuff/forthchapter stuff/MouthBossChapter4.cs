using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouthBossChapter4 : MonoBehaviour
{
    [SerializeField] private Transform player;
    

    [SerializeField] private Animator anBoss;
    
    [SerializeField] private float timeBetweenAttacks,timeBetweenAttacksMax;
    [SerializeField] private int fase = 1;
   
    private int chooseAttack,lastAttack;
    
    [Header("things for attaks")]
    [SerializeField] private Transform teeth;
    private Animator anteeth;
    public Transform[] eachTeethPosition;
    public GameObject toothThrow;
    [SerializeField] private float maxDistance,speedMoveTeeth,distanceForAttack,waitForAttackWithToothAgain;
    private Vector2 teethOriginalPos,teethMaxPos;

    [Space]
    [SerializeField] private LiquidSpawner l1, l2;
    [SerializeField] private float minForce, MaxForce,secsWait;
    [Space]
    [SerializeField] private Transform[] raigs;
    [SerializeField] private string[] optionsRaigAttack;
    [Space]
    [SerializeField] private GameObject[] fallingObjectsFromSky;
    [SerializeField] private float Ypos,minX, maxX, timeBetweenFallOb, timeBetweenFallObRand;
    [SerializeField] private int amountFall, amountFallRand;
    [Space]
    [SerializeField] private Transform transCenterLazerBall;
    [SerializeField] private int amountObjOut, amountObjOutRand;
    [SerializeField] private float timeBtw, timeBtwRand;

    [Space]
    [SerializeField] private Transform spinnyThing;
    [SerializeField] private float speedSpin;
    [SerializeField] private Transform[] raigsSpin;
    [SerializeField] private string[] optionsRaigAttackSpin;

    [Space]
    [SerializeField] private GameObject ballsFase1, ballsFase2;
    private Vector3[] vecGrav = { new Vector3(-6.93f, -6.93f, 0f), new Vector3(0f, -9.81f, 0f), new Vector3(6.93f, -6.93f, 0f), new Vector3(0f, -9.81f, 0f), new Vector3(0f, -9.81f, 0f), new Vector3(0f, -9.81f, 0f), new Vector3(0f, -9.81f, 0f), new Vector3(0f, -9.81f, 0f), new Vector3(0f, -9.81f, 0f), new Vector3(0f, -9.81f, 0f), new Vector3(0f, -9.81f, 0f) };
    private int num_attacks;


    private bool attacking = false,attacking2 = false;

    public int delTest = 0;

    [Space]
    public float rotateVelocity = 8f;

    public GameObject liquidUnderneathFase3;

    
  
    void Start()
    {
      
        teethOriginalPos = teeth.position;
        teethMaxPos = teethOriginalPos;
        teethMaxPos.y += maxDistance;
        anteeth = teeth.GetComponent<Animator>();
        anteeth.Play("TeethFullyOff");
        Boss();
   

    }
    
    
   void Boss()
    {
        //i dont agree not ienumerator
       
        if(fase <3)
        {
            StartCoroutine(Fase(3,0, 6));
            
        }
        else if(fase < 6)
        {
            StartCoroutine(Fase(6, 0, optionsRaigAttack.Length));
            StartCoroutine(PeriodicCenterFase(6, 0,4));
        }
        else if(fase < 9)
        {
            StartCoroutine(Fase(9, 4, optionsRaigAttack.Length));
            StartCoroutine(PeriodicCenterFase(9, 4, optionsRaigAttackSpin.Length));
            //ADD LIQUID UNDERNEATH
            //StartCoroutine(RotateFase3());
        }
        else if(fase == 9)
        {
            FinishBoss();
        }
        
    }

    //change
    private IEnumerator Fase(int _fase,int minOption, int maxOption)
    {
        while (fase < _fase)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(timeBetweenAttacks, timeBetweenAttacksMax));
            num_attacks += 1;
            if (num_attacks > Random.Range(4, 7))
            {
                num_attacks = 0;
                StartCoroutine(ToothAttack());
            }
            else
            {
                int chooseAt = UnityEngine.Random.Range(minOption, maxOption);
                attacking = true;
                Debug.Log(delTest);

                StartCoroutine(RaigAttack(optionsRaigAttack[delTest], raigs,1));
                
                
            }
            yield return new WaitWhile(isAttacking);
            
        }
        Boss();
        
    }
    private bool isAttacking()
    {
        return attacking;
    }
    private bool isAttacking2()
    {
        return attacking2;
    }



    private void FaseTransition1()
    {
        //remove balls
    }
    private IEnumerator PeriodicCenterFase(int _fase, int minOption, int maxOption)
    {
        yield return new WaitForSeconds(timeBetweenAttacks / 2 + 1);
        while (fase < _fase)
        {
            Debug.Log("Entered");
            yield return new WaitForSeconds(UnityEngine.Random.Range(timeBetweenAttacks, timeBetweenAttacksMax));
            int random = Random.Range(1, 4);
            
            int chooseAt = UnityEngine.Random.Range(minOption, maxOption);
            attacking2 = true;
            StartCoroutine(RaigAttack(optionsRaigAttackSpin[chooseAt], raigsSpin,2));
            delTest++;
            yield return new WaitWhile(isAttacking2);

        }
    }
    private void FaseTransition2to3()
    {
        liquidUnderneathFase3.SetActive(true);
    }
 
    private IEnumerator ToothAttack()
    {
        //this could be the liquid for example or some other thing
        /*
        float random0to1 = Random.Range(0, 1);
        teeth.position = Vector2.Lerp(teethOriginalPos, teethMaxPos, random0to1);
        //anBoss.Play("AnimationName");*/
        anteeth.Play("TeethOff");
        int x = 0;
        float distMin = float.PositiveInfinity;
        float distMiny = 0;
        attacking = true;
        attacking2 = true;
        for (int i = 0; i < eachTeethPosition.Length; i++)
        {
            float dist =Vector2.Distance(eachTeethPosition[i].position , player.position);
           
            if( dist < distMin)
            {
                x = i;
                distMin = dist;
                distMiny = Mathf.Abs(eachTeethPosition[i].position.y - player.position.y);


            }
        }
        if(distMiny < distanceForAttack)
        {
            anteeth.Play("TeethAttack1");
            yield return new WaitForSeconds(2f);
            
            for(int i = 0;i<3;i++){
                Instantiate(toothThrow, eachTeethPosition[UnityEngine.Random.Range(2, eachTeethPosition.Length - 3)].position, toothThrow.transform.rotation).SetActive(true);

            }


            anteeth.Play("TeethFullyOff");
            attacking2 = true;
            yield return new WaitForSeconds(5f);
            attacking2 = false;
            attacking = false;
            
           
          
        }
        else
        {
            
            Vector2 pos = teeth.position;
            pos.y += (eachTeethPosition[x].position.y - player.position.y) * Time.fixedDeltaTime * speedMoveTeeth*-1f;
            teeth.position = pos;
            yield return new WaitForFixedUpdate();
            StartCoroutine(ToothAttack());
        }

        

    }
    //delete
    private void ThrowLiquid()
    {
        l1.enabled = true;
        l2.enabled = true;
        StartCoroutine(ChangeForce());
    }
    //delete
    private IEnumerator ChangeForce()
    {
        float E = UnityEngine.Random.Range(minForce, MaxForce);
        l1.addForce.x = E;
        l2.addForce.x = -E;
        yield return new WaitForSeconds(secsWait);
        StartCoroutine(ChangeForce());
    }


    private IEnumerator RaigAttack(string optionsRaigs,Transform[] _raigs,int isAttacking1or2)
    {
       // int chooseAt = UnityEngine.Random.Range(0, optionsRaigAttack.Length);
        int n = 0;
        float spin = 0;
        foreach (char c in optionsRaigs)
        {

            if(c == 'T')
            {
                _raigs[n].gameObject.SetActive(true);
            }
            else if(c == 'F')
            {
                _raigs[n].gameObject.SetActive(false);
            }
            else if(c == ' ')
            {
               
                yield return new WaitForSeconds(2f);
                foreach (Transform go in _raigs)
                {
                    go.gameObject.SetActive(false);
                }
                n = -1;
            }
            else if(c == '.')
            {
                yield return new WaitForSeconds(.25f);
                n = -1;
            }
            else if (c == 'R')
            {
                Debug.Log("R");
                spin = speedSpin;
                n = -1;
            }
            else if (c == 'L')
            {
                Debug.Log("L");
                spin = -speedSpin;
                n = -1;
            }
            else if (spin != 0)
            {
                Debug.Log("Spin");

                float T = Time.time + 1f;
                Vector3 rot = spinnyThing.rotation.eulerAngles;
                float baseSpeed = (c >= '0' && c <= '9') ? (c - '0') : 1f;
                float r = baseSpeed * spin;

                while (T > Time.time)
                {
                    yield return null;
                    rot.z += r * Time.deltaTime * rotateVelocity;
                    spinnyThing.rotation = Quaternion.Euler(rot);

                }
                spin = 0;
            }

            n += 1;
        }
        if(isAttacking1or2 == 1)
        {
            attacking = false;
        }
        else if(isAttacking1or2 == 2)
        {
            attacking2 = false;
        }
       
       // raig.position = new Vector2(player.position.x, raig.position.y);
       // raig.gameObject.SetActive(true);
    }
    //idk what to do
    private IEnumerator FallingObjects()
    {
        int maxamount = UnityEngine.Random.Range(amountFall, amountFallRand);
        for (int i = 0; i < maxamount; i++)
        {
            Instantiate(fallingObjectsFromSky[UnityEngine.Random.Range(0, fallingObjectsFromSky.Length)], new Vector3(UnityEngine.Random.Range(minX, maxX), Ypos, 0f), Quaternion.Euler(0, UnityEngine.Random.Range(0,360),0));
            yield return new WaitForSeconds(UnityEngine.Random.Range(timeBetweenFallOb, timeBetweenFallObRand));
        }
    }
    private IEnumerator SpinningBallLazersCenter()//bad i think it wont be fun
    {
        //play animation ball appears center
        //change variables
        int maxamount = UnityEngine.Random.Range(amountFall, amountFallRand);
        for (int i = 0; i < maxamount; i++)
        {
            // change objects
            Instantiate(fallingObjectsFromSky[UnityEngine.Random.Range(0, fallingObjectsFromSky.Length)], transCenterLazerBall.position,Quaternion.Euler(0,0,UnityEngine.Random.Range(0,360)));
            yield return new WaitForSeconds(UnityEngine.Random.Range(timeBtw, timeBtwRand));
        }
    }
    //delete holdables after everry faze
    //change gravity in fase 3???


    private IEnumerator RotateFase3()
    {
        float t;
        Vector3 grav;
        float velocity = 5f;
        while (fase < 9)
        {
            grav = Physics2D.gravity;
            Vector3 endPos = vecGrav[Random.Range(0, vecGrav.Length)];
            t = 0f;
            while (t<1)// this creates error obviously
            {
               
                Physics2D.gravity = Vector3.Slerp(grav, endPos, t);
                t += Time.fixedDeltaTime/velocity;
                //an[anName].time = t;
                //an.Play(anName);
                yield return new WaitForFixedUpdate();
                
                //Debug.Log(Physics2D.gravity);
                
            }
            
        }
        
        
    }

    
    
   




    //FASES: (remember teeth)
/*FASE 1:
 * Pattern attacks
 * liquid underneath
 */



/*FASE 2:
 * Pattern attacks (more and speed up)
 * liquid underneath
 * Center ball (small lazers?)
 * less holdaballs
 */
/*FASE 3:
 * Pattern attacks (*)
 * liquid underneath
 * center ball lazer lazers
 * even less holdaballs
 * gravity changes :)
 */


    public void NextFase()
    {
        fase += 3;
        if(fase==6){
            FaseTransition2to3();
        } 
    }

    public void HitBottomOfStage()
    {
        //restart boss
    }

    public void FinishBoss()
    {

    }
}
