using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2StartFight : MonoBehaviour
{
    public GameObject activate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            activate.SetActive(true);
           
            Destroy(gameObject);
        }
       
    }
}
