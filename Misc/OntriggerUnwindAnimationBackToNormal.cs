using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OntriggerUnwindAnimationBackToNormal : MonoBehaviour
{
    public void Rewind(){
        GetComponent<Animator>().SetFloat("Speed", 1f);
    }
}
