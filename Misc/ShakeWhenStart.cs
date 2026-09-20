using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShakeWhenStart : MonoBehaviour
{
    [SerializeField] private float time, amp, freq, timeReduce;
    //delete these variables after demo
    public bool demo;
    public GameObject button;
  
    
    void Start()
    {
        GameManager.gameManager.ShakeCam(time, amp, freq,timeReduce);
    }
    public void LoadNextSCENE()
    {
        if (demo){ 
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button);
            GameManager.gameManager.demoFinishedDELETEAFTER = true;

            return;
        }   
        GameManager.gameManager.LoadNextScene();
    }
    public void PlayerStill()
    {
        GameManager.gameManager.player.GetComponent<Rigidbody2D>().isKinematic= true;
    }
    public void PlayerNotStill()
    {
        GameManager.gameManager.player.GetComponent<Rigidbody2D>().isKinematic = false;
    }
    

}
