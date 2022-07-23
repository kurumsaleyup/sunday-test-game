using System;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public event Action<PointerEventData> onPointerDown;
    public event Action<PointerEventData> onDrag;
    public event Action<PointerEventData> onPointerUp;


    #region Pointer Events

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp?.Invoke(eventData);
    }

    #endregion
}