using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class C5EndHandAndPush : MonoBehaviour
{
    public holdaball holdaball;
    public moveLeftHand moveLeftHand;
    public GameObject[] turnOff;
    public Transform body, endPos;
    private HingeJoint2D joint2D;
    [Space]
    public float randTimeOffMin = 2f;
    public float randTimeOffMax = 5f, randTimeOnMin = 0.5f, randTimeOnMax = 1f;
    public Color offColor = Color.white;
    private Color nowColor;
    public SpriteRenderer spriteRendererHand;
    public RawImage black;

    void Start()
    {
        //StartCoroutine(TurnOnAndOffHand());
        nowColor = GameManager.gameManager.colorLeft;
        joint2D = holdaball.GetComponent<HingeJoint2D>();   
    }


    IEnumerator TurnOnAndOffHand(){
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randTimeOnMin,randTimeOnMax));
            moveLeftHand.enabled= false;
            holdaball.enabled= false;
            spriteRendererHand.color =  offColor;
            //Rigidbody2D rb = joint2D.connectedBody;
            //joint2D.connectedBody = null;
            for (int i = 0; i <turnOff.Length; i++)
            {
                turnOff[i].SetActive(false);
            }

            yield return new WaitForSeconds(Random.Range(randTimeOffMin,randTimeOffMax));
            moveLeftHand.enabled = true;
            holdaball.enabled= true;
            spriteRendererHand.color = nowColor;
            //joint2D.connectedBody = rb;
            for (int i = 0; i < turnOff.Length; i++)
            {
                turnOff[i].SetActive(true);
            }

           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // holdaball.enabled = false;
            moveLeftHand.enabled = false;
            body.DOMove(endPos.position, 1f).SetEase(Ease.InOutSine);
            black.DOColor(new Color(0, 0, 0, 1), 1f).SetEase(Ease.InOutSine);
            //go dark
        }
    }

    

}
