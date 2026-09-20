using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBoss4 : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
