using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;
public class Scene2WhenPlayerHitStartScene3 : MonoBehaviour
{
    //[SerializeField] private Canvas canv;
    [SerializeField] private AnalogGlitch anGlitch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.attachedRigidbody.AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
            Invoke(nameof(AnalogGlitchOn), 0.1f);
            Invoke(nameof(GlitchEndScene2), 1.2f);
        }
    }
    void AnalogGlitchOn(){

            anGlitch.enabled = true;
    }
    void GlitchEndScene2()
    {

       
        GameManager.gameManager.LoadNextScene();
    }
}
