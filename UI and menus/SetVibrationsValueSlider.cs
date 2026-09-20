using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetVibrationsValueSlider : MonoBehaviour
{
    // Start is called before the first frame update


    public Slider vibrationsSlider;

    private void Start()
    {
        vibrationsSlider.value = PlayerPrefs.GetFloat("vibrations", 1f);
        SetVibrations();
    }
    public void SetVibrations()
    {
        GameManager.gameManager.vibrations = vibrationsSlider.value;
        PlayerPrefs.SetFloat("vibrations", vibrationsSlider.value);
        PlayerPrefs.Save();
    }
}
