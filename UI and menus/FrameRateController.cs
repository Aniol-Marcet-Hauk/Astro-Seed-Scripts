using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameRateController : MonoBehaviour
{
    public TMP_Dropdown fpsDropdown;
    public TMP_Text fpslablel;
    public TMP_Text fpsvalue;

  
    private int[] fpsOptions = { 30, 60, 120, -1 };

    void Start()
    {
       
        //int savedFPSIndex = PlayerPrefs.GetInt("FPS_Index", 1);
        bool savedVSync = PlayerPrefs.GetInt("VSync", 1) == 1;
        
        if (!savedVSync)
        {
            fpsDropdown.interactable = true;
            int appTargetFR = System.Array.IndexOf(fpsOptions, Application.targetFrameRate); ;
            fpsDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("FPSIndex", appTargetFR >= 0 ? appTargetFR : 1));
        }
        else
        {
            fpsDropdown.interactable = false;
            Color labelColor = fpslablel.color;
            labelColor.a = 0.25f;
            fpslablel.color = labelColor;

            Color valueColor = fpsvalue.color;
            valueColor.a = 0.25f;
            fpsvalue.color = valueColor;
            fpsDropdown.SetValueWithoutNotify(3); // Set to "Unlimited" when VSync is on
        }

        
    }

    // Called automatically when the Dropdown value changes
    public void SetFPS(int index)
    {
        ApplySettings(index);
    }

    // Called automatically when the VSync Toggle changes
    public void SetVSync(bool isVSyncOn)
    {
        //PlayerPrefs.SetInt("VSync", isVSyncOn ? 1 : 0);
        if(isVSyncOn)
        {
            fpsDropdown.value = 3;
            fpsDropdown.interactable = false;
            Color labelColor = fpslablel.color;
            labelColor.a = .25f;
            fpslablel.color = labelColor;

            Color valueColor = fpsvalue.color;
            valueColor.a = .25f;
            fpsvalue.color = valueColor;

        }
        else
        {
            fpsDropdown.interactable = true;
            Color labelColor = fpslablel.color;
            labelColor.a = 1f;
            fpslablel.color = labelColor;

            Color valueColor = fpsvalue.color;
            valueColor.a = 1f;
            fpsvalue.color = valueColor;
        }
      
    }

    // The core logic that talks to Unity
    private void ApplySettings(int fpsIndex)
    {
        // Apply VSync first
        //QualitySettings.vSyncCount = isVSyncOn ? 1 : 0;

        // Only apply the target framerate if VSync is disabled.
        // If VSync is on, Unity ignores Application.targetFrameRate anyway.
        if (QualitySettings.vSyncCount == 0)
        {
            Application.targetFrameRate = fpsOptions[fpsIndex];
            PlayerPrefs.SetInt("FPSIndex", fpsIndex);
            PlayerPrefs.Save();
        }
    }
}
