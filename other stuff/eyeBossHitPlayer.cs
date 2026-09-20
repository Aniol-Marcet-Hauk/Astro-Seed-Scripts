using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeBossHitPlayer : MonoBehaviour
{
    public float damage = 4f;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Glitch mini explode effect
        if(collision.tag != "Player")
        {
            return;
        }
     

        Hit(collision);

    }

    public void Hit(Collider2D collision){
        //add effect of glitch


        //...
        eyeBoss eye = FindAnyObjectByType<eyeBoss>();
        if (eye.canDamage == false)
        {
            return;
        }
        eye.canDamage = false;
        Debug.Log(eye.boss3Xp);
        for (int i = 0; i < damage; i++)
        {
            
            Instantiate(eye.boss3Xp.gameObject, collision.transform.position, Quaternion.identity).SetActive(true);

        }

        eye.EnableDamageInvoke(1f);
        GameManager.gameManager.onHit?.Invoke(true);
    }

   



}
