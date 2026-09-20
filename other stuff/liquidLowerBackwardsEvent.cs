using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class liquidLowerBackwardsEvent : MonoBehaviour
{
    //YES AND I DONT CARE
   
    [SerializeField] private Animator an;
    [SerializeField] private GameObject liquidObject;
    public absorberPartWhereLiquidDown[] absorbersTurnedOn;
    public float speedDown = 1f;

    private void Start()
    {
        foreach (var absorber in absorbersTurnedOn)
        {
            absorber.TurnedOn += StartLiquidLower;
        }
        StopLiquidLower();

    }
    public void StartLiquidLower()
    {
        an.SetFloat("moveSpeed", speedDown);
    }
    public void StopLiquidLower()
    {
        an.SetFloat("moveSpeed", 0);
    }
    public void DestroyLiquid() {
        foreach (Transform item in liquidObject.transform)
        {
            item.DOScale(new Vector3(0f, 0f), 0.3f);
        }
        Invoke("DoDestroy", 0.35f);

    }
    public void DoDestroy()
    {
        Destroy(liquidObject);
        Destroy(gameObject);
    }
}
