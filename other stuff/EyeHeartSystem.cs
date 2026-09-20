using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeHeartSystem : MonoBehaviour
{
    public float En, totalEn;
    [SerializeField] private float enUp;
    private BreakWhenPulling bWPulling;

    [SerializeField] private Animator an;
    [SerializeField] private LineRenderer lr;
    public bool boolBreak {get; private set;}
    private void Start()
    {
        bWPulling = transform.GetComponent<BreakWhenPulling>();
        boolBreak = false;
    }
    private void Update()
    {
        lr.SetPosition(1, transform.position);
        an.Play("BatteryDeminish", 0, totalEn- En / totalEn);
        
       // an.Play("BatteryDeminish");
        if (En <= 0f)
        {
            En = 0;
            bWPulling.StartCheckingV(true);
            StartCoroutine(EnUp());
        }
    }
    public void LowerEnB(float amount)
    {
        En -= amount;


    }
    public void AddEnB(float amount)
    {
        En += amount;
        if(En> totalEn) 
        {
            En = totalEn;
        }
    }

    public void SetEn(float En)
    {
        this.En = En;
        totalEn = En;
    }
    private IEnumerator EnUp()
    {
        boolBreak = true;
        while(En < totalEn)
        {
            En += enUp * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        boolBreak = false;
        En = totalEn;
        bWPulling.StartCheckingV(false);
    }
}