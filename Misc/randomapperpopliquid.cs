using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomapperpopliquid : MonoBehaviour
{
   [SerializeField] private Animator an;


    void Start()
    {
        StartCoroutine(pop());
    }
    IEnumerator pop()
    {
        
        yield return new WaitForSeconds(Random.Range(2f, 50f));
        if(gameObject != null)
        {
           
            an.Play("liquidbubble");
            StartCoroutine(pop());
        }
       
    }
    
    
}
