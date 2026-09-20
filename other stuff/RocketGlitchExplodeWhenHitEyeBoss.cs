using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGlitchExplodeWhenHitEyeBoss : MonoBehaviour
{
    public float damage = 4f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Glitch explode effect
        eyeBoss eye = FindAnyObjectByType<eyeBoss>();
        transform.GetComponent<eyeBossHitPlayer>().Hit(collision);
        eye.RestartPupilAttack();
        transform.position = new Vector3(0f, 0.072f, 9f)+eye.transform.position;
        
        gameObject.SetActive(false);
    }
}
