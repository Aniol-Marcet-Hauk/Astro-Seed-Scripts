using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLazerChapter3 : MonoBehaviour
{
    private RaycastHit2D checkhit2;
    public Transform front;
    public LayerMask LM;
    [SerializeField] private GameObject Electric;
    private GameObject Electr;
    private electricity el;
    private LineRenderer lr;
    [Space]

    public float timeActive,timeInactive;
    [Space]
    [SerializeField] private float dis;
    [Header("movement(elypse)")]
    [SerializeField] private float a;
    [SerializeField] private float b, x, y,speed;
    private float X, Y,normalX,normalY, alpha;
    private Transform trans;
    [Space]
    [SerializeField] private Animator an;
    private bool electrecuted = false;
    
    private void Awake()
    {
        Electr = Instantiate(Electric);
        StartCoroutine("OnOff");
        lr = Electr.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        el = Electr.GetComponent<electricity>();
        trans = transform;
    }

    void Update()
    {
        checkhit2 = Physics2D.Raycast(front.position, transform.up,dis, ~LM);
        lr.SetPosition(0, front.position);
        if (checkhit2)
        {
            
            lr.SetPosition(1, checkhit2.point);
            if(Electr.activeInHierarchy == true && checkhit2.transform.tag == "Player" && electrecuted == false)
            {
                Debug.Log("AAA");
                el.Elec();
                electrecuted=true;
                Invoke(nameof(elBackOn), .7f);
            }
        }
        else
        {
            lr.SetPosition(1, front.position+front.up*dis);
        }

        //elyptical rotation
        alpha += speed*Time.deltaTime;
        X = x + (a * Mathf.Cos(alpha * 0.005f));
        Y = y + (b * Mathf.Sin(alpha * 0.005f));
        normalX = (trans.position.x - x) * b / a;
        normalY = (trans.position.y - y) * a / b;

        trans.position = new Vector3(X,Y,0f);
        trans.up = new Vector3(normalX,normalY,0f).normalized;
    }

    private IEnumerator OnOff()
    {
        while (gameObject)
        {
            an.SetBool("Shooting", true);
            yield return new WaitForSeconds(.15f);
            Electr.SetActive(true);
            yield return new WaitForSeconds(timeActive);
            an.SetBool("Shooting", false);
            //yield return new WaitForSeconds(.35f);
            Electr.SetActive(false);
            yield return new WaitForSeconds(timeInactive);
        }
    }
    private void elBackOn()
    {
        electrecuted = false;
    }
   

}
