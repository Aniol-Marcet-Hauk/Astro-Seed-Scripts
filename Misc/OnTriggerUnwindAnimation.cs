using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerUnwindAnimation : MonoBehaviour
{
    public Animator animator;
    public string floatUnwindName = "Speed";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetFloat(floatUnwindName, -5f);
       
    }
}
