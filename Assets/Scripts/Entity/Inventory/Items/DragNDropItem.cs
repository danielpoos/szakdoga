using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDropItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    private Item item;
    private RectTransform rectTransform;
    private Image image;
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
        throw new System.NotImplementedException();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .65f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}