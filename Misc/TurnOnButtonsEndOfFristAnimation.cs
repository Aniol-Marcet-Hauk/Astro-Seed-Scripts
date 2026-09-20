using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TurnOnButtonsEndOfFristAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Play, Options, Quit;
    [Space]
    [SerializeField] private float time;

    private void Start()
    {
        Time.timeScale = 1f;   
        // 1. Check what the GameManager remembers
        bool hasOpened = GameManager.gameManager.hasOpenedGameOnce;

        // 2. Pass it to the Animator (using your exact state logic)
        GetComponent<Animator>().SetBool("HasOpenedGame", hasOpened);

        // 3. Handle the buttons based on the bool
        if (hasOpened == true)
        {
            // We are skipping the animation! Turn the buttons on immediately
            // because the Animation Event won't play to do it for us.
            Play.enabled = true;
            Options.enabled = true;
            Quit.enabled = true;

            if (EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(Play.gameObject);
            }
        }
        else
        {
            // First time opening! Disable buttons and let the Animation Event turn them on later.
            Play.enabled = false;
            Options.enabled = false;
            Quit.enabled = false;
        }
    }
    public void OnButtonsActivate()
    {
        Play.enabled = true;
        Options.enabled = true;
        Quit.enabled = true;
        GetComponent<Animator>().SetBool("HasOpenedGame", true);
        if (GameManager.gameManager.hasOpenedGameOnce == false)
            GameManager.gameManager.hasOpenedGameOnce = true;
         
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(Play.gameObject);
    }
    
    
}
