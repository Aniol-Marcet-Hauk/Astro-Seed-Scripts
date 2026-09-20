using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSounds : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler,
    ISelectHandler,
    ISubmitHandler
{
    private bool hovered = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hovered) return;
        hovered = true;

        SoundManager.instance.PlayButtonHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.instance.PlayButtonClick();
    }

    // For keyboard/controller navigation
    public void OnSelect(BaseEventData eventData)
    {
        SoundManager.instance.PlayButtonHover();
    }
    public void OnSubmit(BaseEventData eventData)
    {
        // Play click when the element is submitted via keyboard/controller
        SoundManager.instance.PlayButtonClick();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }
}
