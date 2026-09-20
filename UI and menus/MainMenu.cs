using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator an;
    [SerializeField] private string optionsAnName;
    [Space]
    [SerializeField] private GameObject[] mainMenuButtons;
    [SerializeField] private GameObject firstOptionsMenuButton;
    [SerializeField] private GameObject firstControlsButton;
    [SerializeField] private GameObject firstVideoSettingsButton;
    [SerializeField] private GameObject firstAudioSettingsButton;
    [SerializeField] private GameObject firstSaveButton;
    [SerializeField] private GameObject modeSelectSave1Button, modeSelectSave2Button, modeSelectSave3Button;
    [SerializeField] private GameObject firstButtonColorChoose;
    private EventSystem eventSystem;


    [Space]
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle vsyncToggle;
    
    private void Start()
    {
        Cursor.visible = false;
        eventSystem = EventSystem.current;
      
        LoadPlayerPrefs();

    }

    private void LoadPlayerPrefs()
    {
        bool isFullScreen = PlayerPrefs.GetInt("fullScreen", 1) == 1;
        bool isVsync =PlayerPrefs.GetInt("VSync", 1) == 1;

        Screen.fullScreen = isFullScreen;
        QualitySettings.vSyncCount = isVsync ? 1 : 0;
        
        fullScreenToggle.SetIsOnWithoutNotify(isFullScreen);
        vsyncToggle.SetIsOnWithoutNotify(isVsync);


    }
    private void Update()
    {
        GameObject currentSelected = eventSystem.currentSelectedGameObject;
        Button but = currentSelected.GetComponent<Button>();
        if (currentSelected == null || currentSelected.activeInHierarchy == false || but != null && but.isActiveAndEnabled == false) // but == null?
        {
            
            if (mainMenuButtons[0].activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(mainMenuButtons[0]);
            }
            else if(firstSaveButton != null && firstSaveButton.activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(firstSaveButton);
            }
            else if (firstOptionsMenuButton != null && firstOptionsMenuButton.activeInHierarchy == true && firstOptionsMenuButton.GetComponent<Button>().isActiveAndEnabled == true)
            {
                
                eventSystem.SetSelectedGameObject(firstOptionsMenuButton);
                
                
               
            }
            else if (firstControlsButton.activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(firstControlsButton);

            }
            else if (firstVideoSettingsButton.activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(firstVideoSettingsButton);
            }
            else if (firstAudioSettingsButton.activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(firstAudioSettingsButton);
            }
            else if(modeSelectSave1Button != null && modeSelectSave1Button.activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(modeSelectSave1Button);
            }
            else if (modeSelectSave2Button != null && modeSelectSave2Button.activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(modeSelectSave2Button);
            }
            else if (modeSelectSave3Button != null && modeSelectSave3Button.activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(modeSelectSave3Button);
            }
            else if (firstButtonColorChoose != null && firstButtonColorChoose.activeInHierarchy == true)
            {
                eventSystem.SetSelectedGameObject(firstButtonColorChoose);
            }

        }
    }


    public void PlayButton()
    {
        
    }
    public void OptionsButton()
    {
        an.Play(optionsAnName);

        foreach (var item in mainMenuButtons)
        {
            item.SetActive(false);
        }
        

    }
    public void setFalseObject(GameObject gObject)
    {
        gObject.SetActive(false);
        
    }
    
    public void playAnimation(string name)
    {
        an.Play(name);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }



    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullScreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetVsync()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("VSync", 1);
            PlayerPrefs.Save();
            
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("VSync", 0);
            PlayerPrefs.Save();
           
        }
    }

  



}
