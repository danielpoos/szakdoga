using UnityEngine;
using UnityEngine.Events;

public class MonsterObject : MonoBehaviour
{
    private MonsterBase monster;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public UnityEvent<int> MonsterHit = new();
    public static UnityEvent<MonsterObject> MonsterDead = new();
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
    public void Flip(bool isFlipped)
    {
        spriteRenderer.flipX = isFlipped;
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
        monster.SetItemDrop();
        if (monster.GetType() == typeof(Leviathan)|| monster.GetType() == typeof(Vampire) || monster.GetType() == typeof(Werewolf))
        {
            boxCollider.size.Set(110, 130);
        }
        else boxCollider.size.Set(67, 130);
    }
    public void TakeDamage(int damage)
    {
        monster.TakeDamage(damage);
    }
    public void Disappear()
    {
        gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile")) {
            MonsterHit.Invoke(other.GetComponentInParent<ProjectileObject>().GetDamage());
            if (monster.IsDead) {
                MonsterDead.Invoke(this);
                Disappear();
            }
        }
    }
}