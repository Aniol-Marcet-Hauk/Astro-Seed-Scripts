using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class MoveDownScrollRect : MonoBehaviour
{
    public float scrollSpeed = 15f; 
    public bool smoothScroll = true;

    private ScrollRect scrollRect;
    private RectTransform viewport;
    private GameObject lastSelected;
    private float targetY;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();

        viewport = scrollRect.viewport != null ? scrollRect.viewport : GetComponent<RectTransform>();

        targetY = scrollRect.content.anchoredPosition.y;
    }

    void Update()
    {

        GameObject selected = EventSystem.current.currentSelectedGameObject;

   
        if (selected == null || selected == lastSelected)
        {
            ApplyScrolling();
            return;
        }

        if (selected.transform.IsChildOf(scrollRect.content))
        {
            lastSelected = selected;
            CalculateTargetPosition(selected.GetComponent<RectTransform>());
        }

        ApplyScrolling();
    }

    private void CalculateTargetPosition(RectTransform target)
    {
        Vector2 targetPos = scrollRect.content.InverseTransformPoint(target.position);

        float viewportHeight = viewport.rect.height;
        float newY = -targetPos.y - (viewportHeight / 2f);

        float minAnchorY = 0f;
        float maxAnchorY = Mathf.Max(0f, scrollRect.content.rect.height - viewportHeight);

        targetY = Mathf.Clamp(newY, minAnchorY, maxAnchorY);
    }

    private void ApplyScrolling()
    {
        if (smoothScroll)
        {
            float newY = Mathf.Lerp(scrollRect.content.anchoredPosition.y, targetY, Time.unscaledDeltaTime * scrollSpeed);
            scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, newY);
        }
        else
        {
            scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, targetY);
        }
    }
}