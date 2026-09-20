using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiSliderSounds : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    ISelectHandler
{
    private Slider slider;
    private bool hovered = false;

    [Header("Slider Tick Settings")]
    [Tooltip("Minimum time (in seconds) between tick sounds to prevent audio spam.")]
    public float soundCooldown = 0.05f;
    private float nextSoundTime = 0f;

    [Tooltip("How much the slider value must change before triggering a sound.")]
    public float valueChangeThreshold = 0.02f;
    private float lastSoundPlayedValue = 0f;

    void Awake()
    {
        slider = GetComponent<Slider>();
        lastSoundPlayedValue = slider.value;

        // Subscribe to the value changed event automatically
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hovered) return;
        hovered = true;

        SoundManager.instance.PlayButtonHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        SoundManager.instance.PlayButtonHover();
    }

    private void OnSliderValueChanged(float newValue)
    {
        // 1. Check if enough time has passed (cooldown)
        // 2. Check if the value has changed enough to warrant a sound (threshold)
        if (Time.unscaledTime >= nextSoundTime && Mathf.Abs(newValue - lastSoundPlayedValue) >= valueChangeThreshold)
        {
            // You might want to create a specific, softer PlaySliderTick() method in your SoundManager!
            SoundManager.instance.PlayButtonClick();

            lastSoundPlayedValue = newValue;
            nextSoundTime = Time.unscaledTime + soundCooldown;
        }
    }

    void OnDestroy()
    {
        // Always clean up listeners to prevent memory leaks
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}