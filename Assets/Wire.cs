using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    private Canvas canvas;
    private LineRenderer linerenderer;
    private Image image;
    private WireManager wireManager;
    public bool LWire;
    public Color CustomColor;
    public bool Success = false;
    private bool Dragging = false;
    private void Awake()
    {
        image = GetComponent<Image>();
        linerenderer = GetComponent<LineRenderer>();
        canvas = GetComponentInParent<Canvas>();
        wireManager = GetComponentInParent<WireManager>();
    }

    private void Update()
    {
        if (Dragging)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out movePos);
            linerenderer.SetPosition(0, transform.position);
            linerenderer.SetPosition(1,
            canvas.transform.TransformPoint(movePos));
        }
        else
        {
           
            if (!Success)
            {
                linerenderer.SetPosition(0, Vector3.zero);
                linerenderer.SetPosition(1, Vector3.zero);
            }
        }
        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, canvas.worldCamera); 
        if (isHovered)
        {
            wireManager.ChosenWire = this;
        }
    }

    public void SetColor(Color imgcolor)
    {
        image.color = imgcolor;
        linerenderer.startColor = imgcolor;
        linerenderer.endColor = imgcolor;
        CustomColor = imgcolor;
    }
    public void OnDrag(PointerEventData eventData)
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!LWire) { return; }
        if (Success) { return; }
        Dragging = true;
        wireManager.DraggedWire = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (wireManager.ChosenWire != null)
        {
            if (wireManager.ChosenWire.CustomColor == CustomColor && !wireManager.ChosenWire.LWire)
            {
                Success = true;
                wireManager.ChosenWire.Success = true;
            }
        }
        Dragging = false;
        wireManager.DraggedWire = null;
    }
}
