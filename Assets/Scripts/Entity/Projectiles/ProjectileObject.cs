using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    private ProjectileBase probase;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void SetProjectile(ProjectileBase pb)
    {
        probase = pb;
    }
    public ProjectileBase GetProjectile()
    {
        return probase;
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
        if (other.GetType() == typeof(MonsterObject)) Destroy(this);
    }
}