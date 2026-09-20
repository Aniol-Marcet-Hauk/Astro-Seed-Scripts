using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FromSaveToPlayerMode : MonoBehaviour
{
    public RectTransform rect, rectFinal;
    public float speed = 5f;
    private Vector3 startPos, finalPos;
    private Vector2 startSize, finalSize;

    [Space]
    public GameObject[] objectsToDisableInPlayerMode;
    public GameObject PlayerModeAndChapterSelect;

    private void Start()
    {
        startPos = rect.anchoredPosition3D;
        finalPos = rectFinal.anchoredPosition3D;
        startSize = rect.sizeDelta;
        finalSize = rectFinal.sizeDelta;
    }

    public void MoveToPlayerMode()
    {      
        Debug.Log("1.1");
        rect.DOAnchorPos(finalPos, speed).SetEase(Ease.OutBack);
        rect.DOSizeDelta(finalSize, speed).SetEase(Ease.OutBack);
        Debug.Log("1.2");
        DisableInPlayerModeObjects(false);
        Invoke(nameof(PlayerModeON), speed);
        Debug.Log("1.3");

    }
    

    public void MoveBackToSave()
    {
        rect.DOAnchorPos(startPos, speed).SetEase(Ease.OutBack);
        rect.DOSizeDelta(startSize, speed).SetEase(Ease.OutBack);
        PlayerModeAndChapterSelect.SetActive(false);
        Invoke(nameof(DisableInPlayerModeObjectsON), speed);
        
    }
    private void DisableInPlayerModeObjectsON()
    {
        DisableInPlayerModeObjects(true);
    }
    private void DisableInPlayerModeObjects(bool b)
    {
        foreach (GameObject obj in objectsToDisableInPlayerMode)
        {
            obj.SetActive(b);
        }
    }
    private void PlayerModeON()
    {
        Debug.Log("1.3");
        PlayerModeAndChapterSelect.SetActive(true);
    }
    public void Reset()
    {
        rect.anchoredPosition3D = startPos;
        rect.sizeDelta = startSize;
        foreach (GameObject obj in objectsToDisableInPlayerMode)
        {
            obj.SetActive(true);
        }

    }
    public void DisableAll()
    {
        foreach (GameObject obj in objectsToDisableInPlayerMode)
        {
            obj.SetActive(false);
        }
        PlayerModeAndChapterSelect.SetActive(false);
    }


}
