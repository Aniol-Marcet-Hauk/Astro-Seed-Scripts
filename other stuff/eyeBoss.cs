using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeBoss : MonoBehaviour //HOW AM I THIS SHIT OF A PROGRAMMNErJIOAUJEKFbdcsa
{

    public float Energy, Energy1, Energy2;
    public EyeHeartSystem GoEnergy, GoEnergy1, GoEnergy2;
    public float energyLoweredPerAttack;
    
    [Space]
    public float distance;

    
    public int rand;


    public bool Attacking = false;
    private bool inAttack = false;

    private Animator an;

    public Transform player;
    public float z;


    public float timerot = 0.5f;
    [Space]
    public GameObject tinyEye;

    public Transform[] eyesPos;
    [Space]

    public float time;
    public float timeExtraAttackDependant = 0;
    public float timelook;
    public float TimeBtwnAttacks = 50f;

    public int amountBalls = 20;
    public GameObject BallInstan;


    public GameObject Pupil;
    public GameObject PupilMask;
    public Transform pupilPos;
    private bool isPupilAttacking = true;
    public float speedTurnPupil;
    public float speedPupil;
    public float lengthPupilAttack = Mathf.Infinity;

    public bool FollowGBallAn;


    [Space]
    public GameObject FALL;
    public GameObject lazersFromEveryW;
    public float lEveryWTime = 2f;
    [Space]
    public GameObject circlyTransform;
    public Transform fallingLazers;
    public float followSpeedFallingLazers = 6f; 

    private int fase = 1;

    private bool wait;
    private List<GameObject> spawnedBabyEyes = new List<GameObject>();
    public bool canDamage = true;


    public Boss3XP boss3Xp;
    public Action OnXpDeath;
 

    private void OnEnable()
    {
        OnXpDeath += HITENEMY;
    }


    void Start()
    {
       
        Pupil.SetActive(false);
        an = gameObject.GetComponent<Animator>();
        time = Time.time +20f;
        FollowGBallAn = true;
      
        fase = 1;
        GoEnergy.SetEn(Energy);
        GoEnergy1.SetEn(Energy1);
        GoEnergy2.SetEn(Energy2);

        boss3Xp.endPos= GoEnergy.transform.position;
    }


    void Update()
    {
        
        timelook = Time.time;
        if (GoEnergy.gameObject.activeSelf == false && fase == 1)
        {
            fase = 2;
            boss3Xp.endPos = GoEnergy1.transform.position;
            //FASE 2!!!
        }
        else if (GoEnergy1.gameObject.activeSelf == false && fase == 2)
        {
            fase = 3;
            boss3Xp.endPos = GoEnergy2.transform.position;
            StartCoroutine(Eyeball_follows_player());
            //FASE 3!!!!!!!
        }
        else if(GoEnergy2.gameObject.activeSelf == false)
        {
            
            Death();
        }
        if (fase == 1 && GoEnergy.boolBreak == true || fase == 2 && GoEnergy1.boolBreak == true || fase == 3 && GoEnergy2.boolBreak == true)
        {
            if(wait == false)
            {
                wait = true;
                SpawnBabyEyes();

            }
            //animation wait
            return;
        }
        else if (wait)
        {
            inAttack = false;
            Attacking = true;
            wait = false;
            DespawnBabyEyes();
        }

        if (Attacking == false)
        {

            if (inAttack == false)
            {
                Look(transform, timerot);
                if (time +timeExtraAttackDependant+UnityEngine.Random.Range(0f,1f) <= Time.time)
                {
                    Attacking = true;
                }


            }


        }

        else if (inAttack == false)
        {

            Attacking = false;
            inAttack = true;
            z = transform.rotation.z;
            /*if (GoEnergy != null)
            {
                GoEnergy.LowerEnB(energyLoweredPerAttack);
                FirstFase();
            }
            else if (GoEnergy1 != null)
            {
                GoEnergy1.LowerEnB(energyLoweredPerAttack);
                SecondFase();
            }
            else if (GoEnergy2 != null)
            {
                GoEnergy2.LowerEnB(energyLoweredPerAttack);
                ThirdFase();
            }
            else
            {
                Death();
            }*/

            switch (fase)
            {
                case 1:
                    GoEnergy.LowerEnB(energyLoweredPerAttack);
                    
                    Invoke(nameof(FirstFase),0.1f);
                    break;
                case 2:
                    GoEnergy1.LowerEnB(energyLoweredPerAttack);

                    Invoke(nameof(SecondFase),0.1f );
                    break;
                case 3:
                    GoEnergy2.LowerEnB(energyLoweredPerAttack);
                    
                    Invoke(nameof(ThirdFase),0.1f);
                    break;

            }
        }
        



    }


    private void FirstFase()
    {

        if (GoEnergy.boolBreak == true)
        {
            time = Time.time + TimeBtwnAttacks;
            return;
        }


        int random= UnityEngine.Random.Range(0,2);

        switch (random)
        {
            case 0:
                StartCoroutine("LazerAttack");
                timeExtraAttackDependant = 0f;
                break;
            case 1:
                StartCoroutine("GBallLazers");
                timeExtraAttackDependant = 0f;
                break;
            
           
           


        }

        time = Time.time + TimeBtwnAttacks;
    }
    private void SecondFase()
    {
        if (GoEnergy1.boolBreak == true)
        {
            time = Time.time + TimeBtwnAttacks;
            return;
        }

        int random =  UnityEngine.Random.Range(0, 7);
        //i need another attack here

        switch (random)
        {
            case 0:
                StartCoroutine("LazerAttack");
                timeExtraAttackDependant = 0f;
                break;
            case 1:
                StartCoroutine("GBallLazers");
                timeExtraAttackDependant = 0f;
                break;
            case 2:
                StartCoroutine(CircleGrowsAttack());
                timeExtraAttackDependant = 0.5f;
                break;
            case 3:
                StartCoroutine(CircleGrowsAttack());
                timeExtraAttackDependant = 0.5f;
                break;

            case 4:
                StartCoroutine(BallSpinny());
                timeExtraAttackDependant = 4f;
                break;
            case 5:
                StartCoroutine(BallSpinny());
                timeExtraAttackDependant = 4f;
                break;
            case 6:
                StartCoroutine(CircleGrowsAttack());
                timeExtraAttackDependant = 0.5f;
                break;

        }

        time = Time.time + TimeBtwnAttacks;
    }
    private void ThirdFase()
    {
        if (GoEnergy2.boolBreak == true)
        {
            time = Time.time + TimeBtwnAttacks;
            return;
        }

        int random = UnityEngine.Random.Range(0, 5);

        switch (random)
        {
            case 0:
                StartCoroutine(LazersFromAllSides());
                timeExtraAttackDependant = 5f;
                break;
            case 1:
                StartCoroutine(BallSpinny());
                timeExtraAttackDependant = 4f;
                break;
            case 2:
                StartCoroutine(FallingLazers());
                timeExtraAttackDependant =5.5f;
                break;
            case 3:
                StartCoroutine(FallingLazers());
                timeExtraAttackDependant = 5.5f;
                break;
            case 4:
                StartCoroutine(LazersFromAllSides());
                timeExtraAttackDependant = 5f;
                break;

        }

        time = Time.time + TimeBtwnAttacks;
    }

    private void Look(Transform tr,float timerotation)
    {
        Vector3 diff = ((player.position - tr.position) * 2f).normalized;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Vector3 dif = new Vector3(0, 0, rot_z);
        Quaternion rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        tr.rotation = Quaternion.Slerp(tr.rotation, rotation, timerotation*Time.deltaTime);
    }

    private IEnumerator LazerAttack()
    {

        inAttack = true;
        rand = UnityEngine.Random.Range(1, 3)*2-3;
        an.SetFloat("direction", rand);
        yield return new WaitForSeconds(0.3f);
        an.Play("lazer4");

        
        yield return new WaitForSeconds(4.5f);

        an.Play("idol");
        inAttack = false;

    }

    private void PlantGrows()
    {

        inAttack = true;
        for (int i = 0; i < 3; i++)
        {

        }
        inAttack = false;
    }

    private void SpawnBabyEyes()
    {
        // Spawn 3 baby eyes at UnityEngine.Random positions
        List<Transform> eyesPos2 = new List<Transform>(eyesPos);
        for (int i = 0; i < 3 && eyesPos2.Count > 0; i++)
        {
            int s = UnityEngine.Random.Range(0, eyesPos2.Count);
            GameObject SuperTinyEye = Instantiate(tinyEye, eyesPos2[s].position, tinyEye.transform.rotation);
            SuperTinyEye.SetActive(true);
            SuperTinyEye.transform.localScale = Vector3.zero;
            SuperTinyEye.transform.DOScale(Vector3.one,.35f).SetEase(Ease.OutBack);
            spawnedBabyEyes.Add(SuperTinyEye);
            eyesPos2.RemoveAt(s);
        }
    }

    private void DespawnBabyEyes()
    {
        foreach (GameObject eye in spawnedBabyEyes)
        {
            if (eye != null)
            {
                eye.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack);
                StartCoroutine(DestroyEye(eye));
            }
        }
        spawnedBabyEyes.Clear();
    }
    IEnumerator DestroyEye(GameObject eye)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(eye);
    }
    
    /*private IEnumerator BabyEyes()
    {
        inAttack = true;
        List<Transform>  eyesPos2 = new List<Transform> (eyesPos);
        // you have to change it to a list 
        List<GameObject> eyes = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            int s = UnityEngine.Random.Range(0, (eyesPos.Length-i));
            //fix quaternion
            
            GameObject SuperTinyEye = Instantiate(tinyEye, eyesPos2[s].position, tinyEye.transform.rotation);
            eyesPos2.Remove(eyesPos2[s]);
            SuperTinyEye.SetActive(true);
            SuperTinyEye.GetComponent<tinyEye>().timeAttacks = UnityEngine.Random.Range(2f, 6f);
            eyes.Add(SuperTinyEye);
            //here eliminate object
        }
        
        inAttack = false;
        yield return new WaitForSeconds(14);
        foreach (GameObject item in eyes)
        {
            Destroy(item);
        }
    } */


    IEnumerator GBallLazers()
    {
        inAttack = true;
        an.Play("EyeBossShowLazerActivate");
        StartCoroutine(Gballlook());
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++)
        {
            FollowGBallAn = false;
            an.Play("GballLazersEyeAn");
            yield return new WaitForSeconds(1.2f);
            FollowGBallAn = true;
            
            yield return new WaitForSeconds(0.2f);
        }
        

        an.Play("LazerActiveToIdleEye");
        inAttack = false;
    }

    private IEnumerator Gballlook()
    {

        float Timel = Time.time;
        
        while(Time.time-Timel < 5f )
        {
            Look(transform, timerot);
            yield return new WaitForFixedUpdate();
        } 
        
    }

    IEnumerator Eyeball_follows_player()
    {

        // do the eyeball follows thingy

        //SO THIS NEEDS TO DESPAWN WHEN HITTING THE PLAYER AND THEN RESPAWN BACK IN THE EYE 
        PupilMask.SetActive(true);
        float I = Time.time ;
        Pupil.transform.parent = null;
        Pupil.SetActive(true);
        Invoke(nameof(turnPupilMaskOff), 3f);
        isPupilAttacking = true;
        while (isPupilAttacking)//(I+lengthPupilAttack> Time.time )
        {
            float turnSpeed = speedTurnPupil;
            /*float dist = Vector3.Distance(Pupil.transform.position, player.position);
            if (dist <= 3f)
            {
                turnSpeed *= 2;
            } */
            Look(Pupil.transform, turnSpeed);
            Pupil.transform.position += Pupil.transform.up * speedPupil * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

   


    }

    private void turnPupilMaskOff()
    {
               PupilMask.SetActive(false);
    }

    /*IEnumerator AttackHeart()
    {
        
        yield return new WaitForSeconds(1f);
        //nothing i guess?

        if (Energy2 < 0)
        {

            Destroy(gameObject);

        }
        else if (Energy1 < 0)
        {
            stage = 3;
        }
        else if (Energy < 0)
        {
            stage = 2;

        }


    }*/


    IEnumerator BallSpinny()
    {
        inAttack = true;
        an.Play("circleSpin");
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < amountBalls; i++)
        {
            float t = (float)i / (amountBalls - 1);
            float waitTime = Mathf.Lerp(.6f, 0.25f, t);
            yield return new WaitForSeconds(waitTime);

            GameObject tempGO = Instantiate(BallInstan);
            tempGO.SetActive(true);
            tempGO.GetComponent<DestroyAfterAn>().StartCoroutine("Demolish");
            tempGO.transform.rotation = pupilPos.rotation;



        }
        an.Play("idol");
        inAttack =false;
    }


    IEnumerator FallingLazers()
    {
        inAttack = true;
        fallingLazers.position = new Vector3(player.position.x, fallingLazers.position.y, fallingLazers.position.z);
        fallingLazers.gameObject.SetActive(true);
        StartCoroutine(FallingLazerFollow());
        yield return new WaitForSeconds(0.5f);
        int random = UnityEngine.Random.Range(14,23);

        for (int i = 0; i < random; i++)
        {
            StartCoroutine(FunctionForFallingLazers(FALL, new Vector3(player.position.x, 125f, 15f), FALL.transform.rotation, 2.5f));
           
            yield return new WaitForSeconds(0.35f);
        }
       
        inAttack = false;
    }
    IEnumerator FallingLazerFollow()
    {
       
        while (inAttack)
        {
            float currentX = fallingLazers.position.x;
            float targetX = player.position.x;

            float newX = Mathf.MoveTowards(currentX, targetX, followSpeedFallingLazers * Time.fixedDeltaTime);
            fallingLazers.position = new Vector3(newX, fallingLazers.position.y, fallingLazers.position.z);

            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1.5f);
        fallingLazers.gameObject.SetActive(false);

    }




    IEnumerator LazersFromAllSides()
    {
        inAttack = true;
        for (int i = 0; i < 24; i++)//remember change 40
        {
            Vector3 vec = new Vector3(UnityEngine.Random.Range(20, 40f), UnityEngine.Random.Range(90f, 120f));

            Quaternion quater = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 180));
            StartCoroutine(FunctionForFallingLazers(lazersFromEveryW, vec, quater, lEveryWTime));
            

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.45f));

        }
        inAttack = false;
    }

    IEnumerator FunctionForFallingLazers(GameObject Inst, Vector3 pos, Quaternion quat, float ti)
    {
        
        GameObject clone = Instantiate(Inst, pos, quat);
        clone.SetActive(true);
        yield return new WaitForSeconds(ti);

        Destroy(clone);
    }


    private IEnumerator CircleGrowsAttack()
    {
        inAttack = true;
        for (int i = 0; i < 2; i++)
        {
            circlyTransform.SetActive(true);
            circlyTransform.transform.rotation = Quaternion.Euler(circlyTransform.transform.rotation.x, circlyTransform.transform.rotation.y, UnityEngine.Random.Range(0, 360));
            yield return new WaitForSeconds(6.2f);
            circlyTransform.SetActive(false);
        }
        inAttack = false;
    }


    private void HollowN_Inspired()
    {




    }

    public void HITENEMY()
    {
        Debug.Log("HITENEMY CALLED");
        if (!canDamage)
        {
            return;
        }
        Debug.Log("HITENEMY Done");
    
        if (GoEnergy.isActiveAndEnabled)
        {
            Debug.Log("en1");
            GoEnergy.AddEnB(1);
        }
        else if (GoEnergy1.isActiveAndEnabled)
        {

            GoEnergy1.AddEnB(1);
        }
        else if (GoEnergy2.isActiveAndEnabled)
        {
            GoEnergy2.AddEnB(1);
        }
     
    }
    public void EnableDamageInvoke(float seconds)
    {
        Invoke(nameof(EnableDamage), seconds);
    }
    private void EnableDamage(){
        canDamage = true;
    }
    public void RestartPupilAttack()
    {
        isPupilAttacking = false;
        Invoke(nameof(EyeFollowsPlayer), 1.5f); 
    }
    private void EyeFollowsPlayer()
    {
        StartCoroutine(Eyeball_follows_player());
    }
    private void Death()
    {
        lengthPupilAttack = 0f;
        canDamage = false;
      
        Debug.Log(boss3Xp);
        for (int i = 0; i < 18; i++)
        {


            GameObject xp = Instantiate(boss3Xp.gameObject,transform.position, Quaternion.identity);
        //    xp.transform.DOScale()
            xp.SetActive(true);


        }
        Destroy(gameObject);
    }
}