using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LoadPlayerPosition : MonoBehaviour
{
    //CHANGE SCRIPT NAME
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject canvas;
    public holdaball holdLeft, holdRight;
    public HingeJoint2D hingeLeft, hingeRight;
    public GameObject bosschapter2;
    private void Awake()
    {
        GameManager.gameManager.playerNOTBODY = transform;
        if(bosschapter2 != null)
        {
            GameManager.gameManager.chapter2Boss = bosschapter2.transform;
        }
        GameManager.gameManager.camObj = cam;
        GameManager.gameManager.holdLeft = holdLeft;
        GameManager.gameManager.holdRight = holdRight;
        GameManager.gameManager.hingeLeft = hingeLeft;
        GameManager.gameManager.hingeRight = hingeRight;

    }
    


}
