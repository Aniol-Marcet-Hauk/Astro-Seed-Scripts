using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartLaunchedPlayer : MonoBehaviour
{
    [SerializeField] private Animator[] ans;
    public void Activate(GameObject go)
    {
        go.SetActive(true);
    }
    private void Start()
    {
        if (GameManager.gameManager.cutscene >= 1)
        {
            foreach (var an in ans)
            {
                an.SetBool("SkipAn", true);
            }

        }
    }

    
}
