using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer1Attack : MonoBehaviour
{
    public GameObject Eye;
    private eyeBoss eyeScript;
    private Animator an;
    private void Start()
    {
        eyeScript = Eye.GetComponent<eyeBoss>();
        an = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        an.SetBool("PlayLazer1An", eyeScript.FollowGBallAn);

        if(eyeScript.FollowGBallAn == true)
        {
            transform.rotation = Eye.transform.rotation;
        }
           
        
    }
}
