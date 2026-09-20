using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartAnimationSound : MonoBehaviour
{

   public AudioClip shipOpening;
   public AudioClip robotArmsOut;
 
   public void PlayShipOpeningSound() {
        
        SoundManager.instance.PlaySound(shipOpening,0.7f);

    }
   public void PlayRobotArmsOutSound() {
        SoundManager.instance.PlaySound(robotArmsOut);
    }

    public void IncreaseGameManagerScene() 
    { 
        GameManager.gameManager.cutscene++;
    }




}
