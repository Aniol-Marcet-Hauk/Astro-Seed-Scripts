using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EndOfChapter2 : MonoBehaviour
{
    public Transform pos;
    public Transform boss;
    public Animator an;
    void Start()
    {
        //an = boss.GetComponent<Animator>();
        StartCoroutine(BossFinale());
    }
    IEnumerator BossFinale()
    {
        boss.GetComponent<spiderMain>().enabled = false;
        boss.DOMove(pos.position, 1.5f);
        boss.DORotate(Vector3.zero,1.5f);

        yield return new WaitForSeconds(1.5f);
        an.enabled = true;
        an.SetBool("Boss2End",true);
    }
    
}
