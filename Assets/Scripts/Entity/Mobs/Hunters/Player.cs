using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private HunterBase hunter;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public UnityEvent<int> PlayerHit = new();
    public static UnityEvent PlayerDead = new();
    public HunterBase Hunter { get => hunter; set => hunter = value; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void Flip(bool isFlipped)
    {
        spriteRenderer.flipX = isFlipped;
    }
    public ProjectileBase GetProjectile()
    {
        hunter.Weapon.SetProjectile();
        return hunter.Weapon.Projectile;
    }
    public void TakeDamage(int damage)
    {
        hunter.TakeDamage(damage);
    }
    public void AddToInventory(Item item)
    {
        try
        {
            hunter.Inventory.AddItem(item);
        }catch (Exception) {}
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            PlayerHit.Invoke(other.GetComponent<MonsterObject>().GetAttack());
            //TakeDamage(other.GetComponent<MonsterObject>().GetAttack());
            if (hunter.IsDead)
            {
                PlayerDead.Invoke();
            }
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            AddToInventory(other.transform.GetComponent<ItemOnGround>().Item);
            other.transform.GetComponent<ItemOnGround>().PickUpObject.RemoveAllListeners();
        }
    }
}
