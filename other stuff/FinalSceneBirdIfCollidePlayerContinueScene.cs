using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneBirdIfCollidePlayerContinueScene : MonoBehaviour
{
    public static event Action OnPlayerGrab;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnPlayerGrab?.Invoke();
            Destroy(this);
        }
    }
}
