using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    private MonsterBase monster;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public MonsterBase Monster { get => monster; set => monster = value; }
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public int GetAttack()
    {
        if (monster == null) return 0;
        return monster.Attack;
    }
    public void SetMonster(MonsterBase mb)
    {
        monster = mb;
        spriteRenderer.sprite = monster.Sprite;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.GetType() != typeof(Monster) || other.GetType() != typeof(ItemOnGround)) monster.TakeDamage(other.GetComponent<ProjectileObject>().GetDamage());
        if (other.GetType() == typeof(ProjectileObject)) monster.TakeDamage(other.GetComponentInParent<ProjectileObject>().GetDamage());
    }
}