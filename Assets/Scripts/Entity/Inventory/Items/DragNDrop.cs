using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private InventoryExtended inventory;
    [SerializeField] private CraftObject craftObject;
    private Item item;
    private Image image;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        image = gameObject.GetComponent<Image>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        image.sprite = item.Sprite;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .65f;
        Debug.Log("begin");
        canvasGroup.blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        Debug.Log("end");
        canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}