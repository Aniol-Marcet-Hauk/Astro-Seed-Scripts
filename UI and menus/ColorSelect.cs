using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class ColorSelect : MonoBehaviour,
   IPointerClickHandler,
    ISubmitHandler
{



    public Color color;
    //public Color colorNorm, colorSelected;
    //public GameObject turnOn;
    public bool  left = true;
    public bool selected = false;
    public float unselectedSize, selectedSize;
    private void Start()
    {
        if (selected)
        {
            if(left)
            {
                GameManager.gameManager.colorLeft = color;
            }
            else
            {
                GameManager.gameManager.colorRight = color;
            }
        }
    }
    public void OnSubmit(BaseEventData eventData)
    {
        Select();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    void Select()
    {
        if(left)
        {
            GameManager.gameManager.colorLeft = color;
        }
        else
        {
            GameManager.gameManager.colorRight = color;
        }

        selected = true;
        transform.DOScale(Vector3.one * selectedSize, 0.15f).SetEase(Ease.OutBack);




    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if(left && GameManager.gameManager.colorLeft != color || !left && GameManager.gameManager.colorRight != color)
            {
                selected = false;
                transform.localScale = Vector3.one * unselectedSize;
            }
           
        }
        /*else if (left && GameManager.gameManager.colorLeft == color || !left && GameManager.gameManager.colorRight == color)
        {
            selected = true;
            transform.localScale = Vector3.one * selectedSize;

        }  */
    }
}
