using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaitAndGoToNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public float waitTime;
    private InputManager im;
    private bool opPressed;
    float t,skipTime = 2;
    void Start()
    {
        StartCoroutine(Finish());
        im = InputManager.instance;
        t = 0;
    }
    void Update()
    {
        
        if(im.anyKeyPressed && !opPressed)
        {
            opPressed = true;
            t = Time.time;
        }
        if (!opPressed) return;
        if(t+skipTime < Time.time)
        {
            opPressed = false;
            StopAllCoroutines();
            GameManager.gameManager.animationSkip = true;
            GameManager.gameManager.cutscene = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(!im.anyKeyPressed) 
        {
            opPressed = false;
            t = 0;
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
