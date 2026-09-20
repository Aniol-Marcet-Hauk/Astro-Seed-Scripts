using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleFormLinker : MonoBehaviour
{
  
    public string formUrl = "https://forms.gle/YOUR_FORM_ID";

    public void OpenForm()
    {
        if (!string.IsNullOrEmpty(formUrl))
        {
            Application.OpenURL(formUrl);
            Debug.Log("Opening Google Form: " + formUrl);
        }
        else
        {
            Debug.LogWarning("Google Form URL is empty!");
        }
    }
}
