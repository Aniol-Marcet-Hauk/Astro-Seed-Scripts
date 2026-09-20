using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeForBoss2BecauseAnimationsSuck : MonoBehaviour
{
    public Transform trans, transDuringAn;
    public Transform target, targetDuringAn;
    public spiderMain spidermain;
    private Animator an;
    private void Start()
    {
        an = GetComponent<Animator>();
    }
    public void PlayAn()
    {
        trans = spidermain.trans;
        target = spidermain.target;
        spidermain.trans = transDuringAn;
        spidermain.target = targetDuringAn;
    }
    public void FinishAn()
    {
        spidermain.target = target;
        spidermain.trans = trans;
    }
    private void Update()
    {
        if(an.enabled == false)
        {
            an.enabled = true;
        }
    }
}
