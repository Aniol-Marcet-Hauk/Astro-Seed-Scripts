using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finalsceneturningonhand : MonoBehaviour
{
    private InputManager inputManager;
    private float t = 0f;
    public float speedan = 1f;
    [SerializeField] private Animator anOfPlayer;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.leftTriggerPressed)
        {
            t += Time.deltaTime * speedan;
            if (t >= .97f)
            {
               
                t = .97f;
            }
        
        }
        else if(t > 0f)
        {
            t -= Time.deltaTime* speedan*0.5f;
            if (t < 0f)
            {
                t = 0f; // Ensure t doesn't go below 0
            }
        }
        anOfPlayer.SetFloat("time", t);
    }
    private void OnEnable()
    {
        // 3. Subscribe to the event
        FinalSceneBirdIfCollidePlayerContinueScene.OnPlayerGrab += GetUpScene;
    }

    private void OnDisable()
    {
        // 4. ALWAYS unsubscribe to prevent memory leaks!
        FinalSceneBirdIfCollidePlayerContinueScene.OnPlayerGrab -= GetUpScene;
    }

    private void GetUpScene()
    {
        Debug.Log("yey");
        transform.GetComponent<Animator>().SetBool("GetUpScene", true);
        anOfPlayer.SetBool("GetPlayerUp", true);
    }
}
