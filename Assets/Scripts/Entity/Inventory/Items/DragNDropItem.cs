using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Image;

public class DragNDropItem : MonoBehaviour, IDropHandler/*,IPointerDownHandler, IDragHandler,  IBeginDragHandler, IEndDragHandler*/
{
    private Item item = null;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    public Item Item { get => item; set { item = value; if (value != null) spriteRenderer.sprite = item.GetSprite();} }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("ondrop");
        //if (eventData.pointerDrag != null)
        //{
        //    DragNDrop asd = eventData.pointerCurrentRaycast.gameObject.GetComponent<DragNDrop>();
        //    if (asd.item == null) { asd.item = item; Debug.Log(" null volt "); }
        //    else { this.transform.position = origin; Debug.Log(" nem null volt "); }
        //    Debug.Log(asd.item + " asd " + item);
        //}
    }
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("asd");
    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("asd");
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("asd");
    //}
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("asd");
    //}
    private void Awake()
    {
        boxCollider = transform.GetOrAddComponent<BoxCollider2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
}