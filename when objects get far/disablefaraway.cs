using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablefaraway : MonoBehaviour
{
    private GameObject loadObject;
    private itemloader loadscript;
    private void Start()
    {
        loadObject = GameObject.Find("loaderObject");
        loadscript = loadObject.GetComponent<itemloader>();

        StartCoroutine("addcomponent");
        
    }
    IEnumerator addcomponent()
    {
        yield return new WaitForSeconds(0.1f);
        loadscript.addList.Add(new ActivatorItem { item = this.gameObject });
    }
    
}

