using UnityEngine;
using UnityEngine.Events;

public class ItemOnGround : MonoBehaviour
{
    private Item item;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public bool IsPickedUp = false;
    public UnityEvent<Item> PickUpObject = new();
    public Item Item { get => item; set => item = value; }
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }
    public void Disappear()
    {
        gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUpObject.Invoke(item);
            IsPickedUp = true;
        }
    }
}