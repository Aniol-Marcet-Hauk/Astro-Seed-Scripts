using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;
public class GameManager : MonoBehaviour
{
    
    public static GameManager gameManager { get; private set; }
    void OnEnable()
    {
        if (gameManager != null && gameManager != this) return;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 2. ALWAYS unsubscribe when disabled/destroyed to prevent memory leaks
    void OnDisable()
    {
        if (gameManager != null && gameManager != this) return;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        

        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            gameManager = this;
        }
        DontDestroyOnLoad(gameObject);
        dropDownValue = PlayerPrefs.GetInt("resolutionIndex", -1);
        if (dropDownValue != -1)
        {
            resolutionSet = true;
        }

      
    }
    public bool hasOpenedGameOnce = false;
    public bool gravityIsChanging = false;

    public SaveFileData saveData;
    [Space]
    public Transform playerNOTBODY,player;
    public holdaball holdLeft, holdRight;
    public HingeJoint2D hingeLeft, hingeRight;
    public GameObject camObj;
    [Space]
    public bool isPAUSED,animationSkip;
    private CinemachineBasicMultiChannelPerlin vcam;
    private Coroutine shakyCoroutine;

    //
    [Space]
    public bool bossFight = false;
    public Transform chapter2Boss;
    public int cutscene,
        boss3Fase;

    [Space]
    public ParticleSystem electricityParticle;
    public bool isElectrecuted { get; private set; } = false;
    public Action<bool> onHit;
    [Space]
    public bool resolutionSet = false;
    public int dropDownValue = 0;
    [Space]
    public bool timerOn = false;
    public float  timeHasPassed= 0f;
    public float vibrations = 1f;
    [Space]
    public Color colorLeft = Color.white, colorRight = Color.white;

    public bool demoFinishedDELETEAFTER = false;

    

    //CREATE A SAVE DATA FUNCTION


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        gameManager.demoFinishedDELETEAFTER = false;
        
        if (level <=1 || saveData == null)
        {                   
            if(level==0) timeHasPassed = 0f;
            Debug.Log(1);
            return;
        }
        if(level != saveData.level)
        {
            Debug.Log(2);
            saveData.level = level;
            return;
        }
        //ADDD THING THAT CHECKS FOR ANIMATION
      
        SetUpSaveFile();
        isElectrecuted = false;
        gameManager.colorLeft.a = 1f;
        gameManager.colorRight.a = 1f;
        

    }
    void SetUpSaveFile()
    {

       
        //CUTSCENE THINGS AND DOORS ARE STILL MISSING, ETC
        Vector3 pos = new Vector3();
         
        switch (saveData.level)
        {

            case (int)Level.chapter2:
                bossFight = saveData.chp2.bossFight;
                //whut
               /* pos.x = gameManager.saveData.position[0];
                pos.y = gameManager.saveData.position[1];
                pos.z = gameManager.saveData.position[2];   */
                chapter2Boss.position = pos;
                cutscene = saveData.chp2.cutSceneNum;
                break;
            case (int)Level.chapter3Boss:
                boss3Fase = saveData.chp3Boss.fase;
                break;
            case (int)Level.chapter4:
                cutscene = saveData.chp4.cutSceneNum;
               
                pos.x = gameManager.saveData.chp4.gravity[0];
                pos.y = gameManager.saveData.chp4.gravity[1];
                pos.z = gameManager.saveData.chp4.gravity[2];
                Physics2D.gravity = pos;


                break;
            default:
                break;
        }
        timeHasPassed = saveData.timer;
        cutscene = saveData.cutScene;
        StartCoroutine(SetPlayerPos());
    }
    IEnumerator SetPlayerPos()
    {
    
        while (playerNOTBODY == null)
        {
            yield return new WaitForEndOfFrame();
        }
        Vector3 pos = new Vector3();
        pos.x = gameManager.saveData.position[0];
        pos.y = gameManager.saveData.position[1];
        pos.z = gameManager.saveData.position[2];
     
        playerNOTBODY.position = pos;
        camObj.SetActive(true); 
    }
   
    public void ShakeCam( float _time, float _amp, float _freq,float _timeReduce, float _timeIncrease = 0)
    {
        if(vcam == null)
        {
            var vcamObj = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
            if (vcamObj == null)
                return;
            vcam = vcamObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (vcam == null)
                return;
            
        }

        // If a shaky coroutine is already running, stop it and reset gains before starting a new one.
        if (shakyCoroutine != null)
        {
            StopCoroutine(shakyCoroutine);
            shakyCoroutine = null;
            if (vcam != null)
            {
                vcam.m_AmplitudeGain = 0f;
                vcam.m_FrequencyGain = 0f;
            }
        }

        shakyCoroutine = StartCoroutine(ShakyGM(_time, _amp, _freq,_timeReduce,_timeIncrease));
    }
    private IEnumerator ShakyGM(float _waitFS,float _strengthAmplitude, float _strengthFrequency, float _reduceTime = 0.1f, float _increaseTime = 0)
    {
       
        if (vcam == null)
            yield break;

      
        if (_increaseTime > 0f)
        {
            float t = 0f;
            float invInc = 1f / _increaseTime;
            while (t < _increaseTime)
            {
                t += Time.fixedDeltaTime;
                float normalized = Mathf.Clamp01(t * invInc);
                vcam.m_AmplitudeGain = _strengthAmplitude * normalized *vibrations;
                vcam.m_FrequencyGain = _strengthFrequency * normalized * vibrations;
                yield return new WaitForFixedUpdate();
            }
        }

       
        vcam.m_AmplitudeGain = _strengthAmplitude * vibrations; ;
        vcam.m_FrequencyGain = _strengthFrequency * vibrations; ;

        // Hold full strength for the wait duration
        if (_waitFS > 0f)
            yield return new WaitForSeconds(_waitFS);

        // REDUCE PHASE: ramp down from full -> 0 over _reduceTime
        if (_reduceTime > 0f)
        {
            float t = _reduceTime;
            float invRed = 1f / _reduceTime;
            while (t > 0f)
            {
                t -= Time.fixedDeltaTime;
                float normalized = Mathf.Clamp01(t * invRed);
                vcam.m_AmplitudeGain = _strengthAmplitude * normalized * vibrations; ;
                vcam.m_FrequencyGain = _strengthFrequency * normalized * vibrations; ;
                yield return new WaitForFixedUpdate();
            }
        }

        // Ensure fully off
        vcam.m_AmplitudeGain = 0f;
        vcam.m_FrequencyGain = 0f;

        // Mark coroutine finished
        shakyCoroutine = null;
    }
    public void ShakeHands(float AG, float FG, float WaitFS)
    {
        if (vcam == null)
        {
            var vcamObj = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
            if (vcamObj == null)
                return;
            vcam = vcamObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (vcam == null)
                return;

        }
        StartCoroutine(Shaky(AG, FG, WaitFS));
    }
    private IEnumerator Shaky(float AG, float FG, float WaitFS)
    {
      
        // If ShakyGM is running, do nothing.
        if (shakyCoroutine != null)
            yield break;
  
       
        vcam.m_AmplitudeGain = AG * vibrations; ;
        vcam.m_FrequencyGain = FG * vibrations; ;
        yield return new WaitForSeconds(WaitFS);
        vcam.m_AmplitudeGain = 0;
        vcam.m_FrequencyGain = 0;
    }
    /*private void resetShake()
    {

    } */

    public void StartGlitchEffect(Vector2 pos, float rotation)
    {
        if(electricityParticle == null)
        {
            Debug.LogWarning("Electricity particle system is not assigned.");
            return;
        }
        electricityParticle.transform.position = pos; 
        electricityParticle.transform.rotation = Quaternion.Euler(rotation,90,-90);
        electricityParticle.Play();
    }

    public void LoadNextScene()
    {
        cutscene = 0;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadNextScene(int _scene)
    {
        cutscene = 0;
        //set next position to ???? whatever the next pos is lol
        SceneManager.LoadScene(_scene);
    }
    public void SetElectrecuted(float frezeTime)
    {
         isElectrecuted = true;
         Invoke(nameof(SetElecFalse), frezeTime);


    }
    public void SetElecFalse()
    {
        isElectrecuted = false;

    }

    void OnSceneUnloaded(Scene scene)
    {
        if(scene.buildIndex <= 1 || saveData == null)
        {
            if (scene.buildIndex == 0) hasOpenedGameOnce = true;

            return;
        }
        timeHasPassed += Time.timeSinceLevelLoad;
     


    }



}

