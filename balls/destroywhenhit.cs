using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroywhenhit : MonoBehaviour
{
    public bool hit;
    private SpriteRenderer spr;
    private Collider2D c2d;
    public float time;
    public float timeafter;
    [SerializeField] private bool turnOffSprite = true;
    [SerializeField] private Animator an;

    private bool Hitter;
    private void Start()
    {
        Hitter = false;
        hit = false;   
        if(turnOffSprite == true)
        {
            spr = gameObject.GetComponent<SpriteRenderer>();
        }
        
        c2d = gameObject.GetComponent<Collider2D>();
        if (an == null)
        {
            an = gameObject.GetComponent<Animator>();
            an.enabled = false;
        }
        if(an == null)
        {
            an = gameObject.GetComponentInChildren<Animator>();
        }
        
       
        an.SetFloat("Speed", 3f/time);
        an.SetBool("Destroying", false);

        Invoke(nameof(EnableAn), Random.Range(0f, 1f));

    }
    private void EnableAn()
    {
        an.enabled = true;
    }
    private void Update()
    {
        if (Hitter == true)
        {
            hit = false;
        }
    }
    void FixedUpdate()
    {
        if(Hitter == true)
        {
            hit = false;
        }
        if (hit == true )
        {
            if(Hitter == false)
            {
                StartCoroutine("hitter");
                
                Hitter = true;
            }
            
        }
        
        
    }
    IEnumerator hitter()
    {
        
        an.SetBool("Destroying", true);
        yield return new WaitForSeconds(time);
        if (turnOffSprite == true)
        {
            spr.enabled = false;
        }
        c2d.enabled = false;
        
        an.SetBool("Destroying", false);
        
        yield return new WaitForSeconds(timeafter);
        Hitter = false;
        an.Play("appear");
        if (turnOffSprite == true)
        {
            spr.enabled = true;

        }
        c2d.enabled = true;
        

    }

}
