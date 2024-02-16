using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Action<PointerEventData> OnEnterHandler = null;
    public Action<PointerEventData> OnExitHandler = null;
    public Action<PointerEventData> OnClickHandler = null;

    public void OnPointerEnter(PointerEventData eventData) => OnEnterHandler?.Invoke(eventData);    

    public void OnPointerExit(PointerEventData eventData) => OnExitHandler?.Invoke(eventData);

    public void OnPointerClick(PointerEventData eventData) => OnClickHandler?.Invoke(eventData);
}
