using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color backgroundColor;

    private void Awake()
    {
        backgroundColor = backgroundImage.color;

        // To check if the inspector detect RectTransform
        if(dragRectTransform == null)
        {
            dragRectTransform = transform.parent.GetComponent<RectTransform>();
        }

        // To check if the inspector detect canvas
        if (canvas == null)
        {
            Transform testCanvasTransform = transform.parent;
            while (testCanvasTransform != null)
            {
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if(canvas != null)
                {
                    break;
                }
                testCanvasTransform = testCanvasTransform.parent;
            }
        }
    }

    // When start drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Reduce alpha of the backgroundColor
        backgroundColor.a = .4f;
        backgroundImage.color = backgroundColor;
    }
    
    // Drag the window
    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Dragging");
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // When end drag
    public void OnEndDrag(PointerEventData eventData)
    {
        // Increase alpha of the backgroundColor
        backgroundColor.a = 1f;
        backgroundImage.color = backgroundColor;
    }

    // Bring the selected window to front
    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTransform.SetAsLastSibling();
    }
}
