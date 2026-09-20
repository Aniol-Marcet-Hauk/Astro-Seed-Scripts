using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SaveLoaderChangeForDemo : MonoBehaviour
{
    private SaveLoader sl;
    public TMP_Text text;
    public string newGame, continueGame;
    public GameObject createNew;
    private InputManager inputManager;
    private FromSaveToPlayerMode fromSaveToPlayerMode;
    public Animator rocket;

    void Start()
    {
        sl = GetComponent<SaveLoader>();
        inputManager = InputManager.instance;
        fromSaveToPlayerMode = GetComponent<FromSaveToPlayerMode>();
        Debug.Log(fromSaveToPlayerMode);

    }
    private void Update()
    {
        if(sl.dataBool == false)
        {
            createNew.SetActive(false);
            text.text = newGame;
        }
        else
        {
            createNew.SetActive(true);
            text.text = continueGame;
        }

    }
    public void Click()
    {
        if (inputManager.playerMode == 0)
        {
            fromSaveToPlayerMode.DisableAll();
            StartCoroutine(WaitAndLoad());
            rocket.Play("RocketRun");
            
            
            return;
            
        }
        Debug.Log("Moving to player mode");
        fromSaveToPlayerMode.MoveToPlayerMode();


    }
    public void LoadWait()
    {
        fromSaveToPlayerMode.DisableAll();
        StartCoroutine(WaitAndLoad());
        rocket.Play("RocketRun");
    

    }

    private IEnumerator WaitAndLoad()
    {
        Debug.Log("Waiting for 5 seconds before loading the game...");
        yield return new WaitForSecondsRealtime(6.5f);
        sl.OnLoad();
    }
}
