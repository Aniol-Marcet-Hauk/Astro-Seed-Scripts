using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueObUseOptimisation : MonoBehaviour
{
    //at the end i don't use this for optimization
    public GameObject connected;
    public float connectedReactivateTime = 1f;

    public Animator an;
    // Update is called once per frame
    private bool AlreadyActive = false;
    public float timeBeforeReactivation = 6f;
    //remember to add the tag.item thingy majig
    void Update()
    {
        if(connected.activeSelf == false)
        {
            //here do sspwaning sistem
            //probably simpler to just parent the square to connected
           
            
            an.SetBool("Open", true);
            StartCoroutine("ReActivate");
            if (AlreadyActive == false)
            {
                AlreadyActive = true;
                //either do it with an animation or with code and parenting a part of the map
            }


        }
    }


    IEnumerator ReActivate()
    {
        yield return new WaitForSeconds(timeBeforeReactivation);
        an.SetBool("Open", false);
        yield return new WaitForSeconds(connectedReactivateTime);
        connected.SetActive(true);
        
        //add collider thing where it turns into a trigger if player still inside
        //and then turn player into static object maybe hands aswell? just slowly move it to wherever we say leaving position is
    }
}
