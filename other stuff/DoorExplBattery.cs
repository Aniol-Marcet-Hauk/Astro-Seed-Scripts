using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExplBattery : MonoBehaviour
{
    //still need to add that if the eye hits energy goes back ups
    [SerializeField] private EyeHeartSystem battery;
    [Space]
    [SerializeField] private float startEn;
    [SerializeField] private float lowerEn;
    public tinyEye tinyE;
    private bool at;
    
    void Start()
    {
        at = false;
        battery.SetEn(startEn);
    }

    // Update is called once per frame
    void Update()
    {
        if(tinyE.attacking == true && at == false)
        {
             battery.LowerEnB(lowerEn);
             at = true;
               
        }
        else
        {
            at =false;
        }
    }
}
