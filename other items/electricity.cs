using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class electricity : MonoBehaviour
{
    public bool HasHappened;
    private holdaball hold;
    private holdaball hold2;
    public float freezeTime;
    
    private HingeJoint2D hinge;
    private HingeJoint2D hinge2;
    public bool ElecOrGlitch = true;
    public float timeBeforeAllowHoldGlitch = 0.3f;
    [Space]
    public AudioSource audioSource;
    public AudioClip electrecutedClip;

    [Space]
    public bool donotElectrecute = false;

    private void Start()
    {
        HasHappened = false;
        hold = GameManager.gameManager.holdLeft;
        hold2 = GameManager.gameManager.holdRight;
        hinge = GameManager.gameManager.hingeLeft;
        hinge2=    GameManager.gameManager.hingeRight;

    }
    private void OnEnable()
    {
        HasHappened = false;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (donotElectrecute) return;
        if (collision.tag == "Player" && HasHappened == false && hold.electrecuted == false)
        {

            if (GameManager.gameManager.isElectrecuted == false)
            {
                HasHappened = true;
                audioSource.PlayOneShot(electrecutedClip);
  
                GameManager.gameManager.SetElectrecuted(freezeTime);
                StartCoroutine(Electrecute());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (donotElectrecute) return;
        if (collision.collider.tag == "Player" && HasHappened == false && hold.electrecuted == false)
        {
            if (GameManager.gameManager.isElectrecuted == false)
            {
                HasHappened = true;
                audioSource.PlayOneShot(electrecutedClip);

                GameManager.gameManager.SetElectrecuted(freezeTime);
                StartCoroutine(Electrecute());
            }
        }
    }
    public void Elec()
    {
        StartCoroutine(Electrecute());
    }
    private IEnumerator Electrecute()
    {
         
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(freezeTime);
        Time.timeScale = 1.0f;

        hold.electrecuted = true;
        hold2.electrecuted = true;
        GameManager.gameManager.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        hinge.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        hinge2.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        hinge.enabled = false;
        hinge2.enabled = false;
        GameManager.gameManager.onHit?.Invoke(true);
        if (ElecOrGlitch == true)
        {
            Vector3 toPlayer = GameManager.gameManager.player.position - transform.position;
            float angleDeg = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
            
            GameManager.gameManager.StartGlitchEffect(transform.position, -angleDeg);
        }
        yield return new WaitForSeconds(0.5f);
        HasHappened = false;

        if(ElecOrGlitch == false)
        {
            //i mean this has a bug. If the script is turned off before the waitforsecondsrealtime ends, the electrecuted bools will never be set to false. Until the player touches the ground
            yield return new WaitForSecondsRealtime(timeBeforeAllowHoldGlitch);
            hold.electrecuted = false;
            hold2.electrecuted = false;
        }
        
    }
}
