using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler//, IDropHandler
{
    //[SerializeField] private InventoryExtended inventory;
    //[SerializeField] private CraftObject craftObject;
    //private DragNDropItem selectedItem;
    //private CanvasGroup canvasGroup;
    private Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 origin;
    private Image image;
    private Item item = null;
    public UnityEvent<Item> ItemDrop = new();
    public Item Item { get => item; set => item = value; }
    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        image = gameObject.GetComponent<Image>();
        //canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    public void SetCanvas(Canvas c)
    {
        canvas = c;
    }
    public void SetItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("nullitem");
            return;
        }
        this.item = item;
        image.sprite = item.GetSprite();
        //transform.Find("Renderer").GetComponent<SpriteRenderer>().sprite = item.GetSprite();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerClick != null)
        {
            origin = eventData.pointerClick.GetComponent<Transform>().position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up"); ;
    }
    //public void OnDrop(PointerEventData eventData)
    //{
    //    Debug.Log("ondrop");
    //    if (eventData.pointerDrag != null)
    //    {
    //        GameObject itemImage = eventData.pointerCurrentRaycast.gameObject;
    //        DragNDrop other = itemImage.GetComponent<DragNDrop>();
    //        if (other.Item == null)
    //        {
    //            ItemDrop.Invoke(item);
    //            other.SetItem(item);
    //            image.sprite = item.GetSprite();
    //            item = null;
    //            transform.Find("Renderer").GetComponent<SpriteRenderer>().sprite = null;
    //            other.transform.Find("Renderer").GetComponentInChildren<SpriteRenderer>().sprite = null;
    //        }
    //        transform.position = origin;
    //    }
    //}
}