using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeBatteryChptr3 : MonoBehaviour
{
    public EyeHeartSystem battery;
    public float energyAdd;
    public tinyEye eye;
    public GameObject heartEye;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            battery.AddEnB(energyAdd);
        }
    }
    
}
