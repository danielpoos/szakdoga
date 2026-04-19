using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    private Item item;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
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
    public Item RecieveItem()
    {
        // destroy this after
        return item;
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetType() == typeof(Player)) Debug.Log(item + " collided with " + other.name+ " " + other.GetType());
    }
}