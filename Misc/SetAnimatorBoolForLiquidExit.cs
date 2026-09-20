using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorBoolForLiquidExit : MonoBehaviour
{
    [SerializeField] private Animator an;
    [SerializeField] private Transform[] listOfTinyLiquids;
    private Vector3[] pos; 
    void Start()
    {
        an.SetBool("IsExit", true);
        pos = new Vector3[listOfTinyLiquids.Length];
        for (int i = 0; i < listOfTinyLiquids.Length; i++)
        {
            pos[i] = listOfTinyLiquids[i].position;
        }
    }

    public void ResetPos()
    {
        for (int i = 0; i < listOfTinyLiquids.Length; i++)
        {
            listOfTinyLiquids[i].position = pos[i];
        }
    }


}
