using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class Absorber : MonoBehaviour
{
    private int layer = 14;
    public int maxCount = 5;
    public float succSpeed = 0.3f;
    private int count = 0;
    private void Start()
    {
        Debug.Log(layer);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layer && count < maxCount)
        {
            count++;
            
            StartCoroutine(absorb(collision.transform));
            collision.enabled = false;
        }
    }
   
    private IEnumerator absorb(Transform trans)
    {
        trans.DOMove(transform.position, succSpeed  );
        trans.DOScale(new Vector3(0.05f, 0.05f), succSpeed);
        yield return new WaitForSeconds(.29f);
        
        
        
        count--;
        Destroy(trans.gameObject);
    }
  

}
