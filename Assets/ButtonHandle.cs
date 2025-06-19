using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandle : MonoBehaviour, IPointerDownHandler
{
    public bool wasPressedThisFrame = false;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        wasPressedThisFrame = true;
    }
}
