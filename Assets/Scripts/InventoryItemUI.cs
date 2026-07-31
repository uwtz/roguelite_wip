using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    PlacedItem placedItem;

    public void Initialize(PlacedItem placedItem)
    {
        this.placedItem = placedItem;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
    }
}
