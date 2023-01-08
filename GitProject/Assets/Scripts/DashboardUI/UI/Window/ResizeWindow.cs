using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizeWindow : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    // The max, min size of the panel
    public Vector2 minSize;
    public Vector2 maxSize;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 currentPointerPosition;
    [SerializeField] private Vector2 previousPointerPosition;

    private void Awake()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }

    public void OnPointerDown (PointerEventData eventData) 
    {
        rectTransform.SetAsLastSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle (rectTransform, eventData.position, eventData.pressEventCamera, out previousPointerPosition);
    }

    public void OnDrag (PointerEventData eventData) 
    {
        if (rectTransform == null)
        {
            return;
        }
            
        Vector2 sizeDelta = rectTransform.sizeDelta;

        RectTransformUtility.ScreenPointToLocalPointInRectangle (rectTransform, eventData.position, eventData.pressEventCamera, out currentPointerPosition);
        Vector2 resizeValue = currentPointerPosition - previousPointerPosition;

        sizeDelta += new Vector2 (resizeValue.x, -resizeValue.y);
        sizeDelta = new Vector2 (Mathf.Clamp (sizeDelta.x, minSize.x, maxSize.x), Mathf.Clamp (sizeDelta.y, minSize.y, maxSize.y));

        rectTransform.sizeDelta = sizeDelta;

        previousPointerPosition = currentPointerPosition;
    }
}
