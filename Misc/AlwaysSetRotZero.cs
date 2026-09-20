using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysSetRotZero : MonoBehaviour
{
    [SerializeField] private bool onlyWhenStart, animationDelay;
    private Animator an;
    private Quaternion rot;
    void Start()
    {
        rot = Quaternion.Euler(Vector3.zero);
        if (onlyWhenStart == true)
        {

            transform.rotation = rot;
            Destroy(this);
        }
        if(animationDelay == true)
        {
            an = transform.GetComponent<Animator>();
            an.enabled = false;
            StartCoroutine(WaitAn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rot;
    }
    IEnumerator WaitAn()
    {
        yield return new WaitForSeconds(Random.Range(0, .5f));
        an.enabled = true;
    }
}
