using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour
{

    [SerializeField] private GameObject liquitPart;
    [SerializeField] private bool startThrowing, disappear;
    [SerializeField] private float rate, randomness, timeToDisappear;
    [SerializeField] private Animator an;
    public Vector2 addForce = Vector2.zero;
    private bool alreadyIs;
    [Space]
    public Vector3 scaleLiquid = Vector3.one;

    private void Start()
    {
        an.SetFloat("Speed", 4 / rate);
       
    }
    private void OnEnable()
    {
        alreadyIs = false;
    }
    private void Update()
    {
        if (startThrowing && !alreadyIs)
        {
            alreadyIs = true;
            StartCoroutine(TimeCheck());
        }
        
    }
    private IEnumerator TimeCheck()
    {

        while (startThrowing)
        {
            float t = rate + Random.Range(-randomness, +randomness);
            an.SetFloat("Speed", 4 / t);// 4 = each animation is 2 sec, there are 2 animations
            yield return new WaitForSeconds(t);
            if (disappear)
            {
                
                GameObject liq = Instantiate(liquitPart, liquitPart.transform.position, Quaternion.Euler(Vector3.zero));
                liq.SetActive(true);
                liq.transform.localScale = scaleLiquid;
                if(addForce != Vector2.zero)
                {
                    liq.GetComponent<Rigidbody2D>().AddForce(addForce);
                }
                
                StartCoroutine(BreakInst(liq));
            }
            else
            {
        
              
                GameObject liq = Instantiate(liquitPart, liquitPart.transform.position, Quaternion.Euler(Vector3.zero));
                liq.transform.localScale = scaleLiquid;
                liq.SetActive(true);
            

            }
            

        }
        an.SetFloat("Speed", 0);
        alreadyIs = false;

    }
    private IEnumerator BreakInst(GameObject liquid)
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(liquid);
    }
}
