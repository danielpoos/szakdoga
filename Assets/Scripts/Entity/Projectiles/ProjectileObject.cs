using UnityEngine;
using UnityEngine.Events;

public class ProjectileObject : MonoBehaviour
{
    private ProjectileBase probase;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private bool isHit = false;
    public ProjectileBase Projectile { get => probase; set => probase = value; }
    public bool IsHit { get => isHit; set => isHit = value; }

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
    public int GetDamage()
    {
        if (probase == null) return 0;
        return probase.Damage;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            isHit = true;
        }
    }
}