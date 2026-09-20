using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWormRotationBack : MonoBehaviour
{
    public float t = 0;
    public Vector3 startPosB, endPosB;
    
    [SerializeField] private GWormRotatation gRot;
    //[SerializeField] private Animation an;
    public float velB;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (t == 1 && gRot.t >= 1 &&
            GameManager.gameManager.gravityIsChanging ==false)
        {
            StartCoroutine("ROTATE");
        }
    }

    private IEnumerator ROTATE()
    {
        GameManager.gameManager.gravityIsChanging = true;
        while (t >= 0)
        {
            Physics2D.gravity = Vector3.Slerp(startPosB, endPosB, t);

            t -= Time.fixedDeltaTime / velB;
            if (gRot.an != null)
            {
                float tclamp = t > 0.1f ? t : .01f;
                gRot.an.SetFloat("motion", tclamp);
           
            }
            yield return new WaitForFixedUpdate();

           // an[gRot.anName].time = t;
            //an.Play(gRot.anName);
        }
        GameManager.gameManager.gravityIsChanging = false;
        t = 0;
        gRot.Reseter();
    }
}
