using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCollideHurtBoss4 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag != "items") { 
             return;
        }
        Destroy(collision.gameObject);
        //Maybe i need to see if only one hit is cool enough, i feel like it should be 3 for fase maybe?
        FindObjectOfType<MouthBossChapter4>().NextFase();
    }
}
