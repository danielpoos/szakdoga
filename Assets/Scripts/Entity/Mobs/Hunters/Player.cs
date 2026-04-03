using UnityEngine;

public class Player
{
    private HunterBase hunter;
    private Inventory inventory;
    private Item currentItem;
    private int money = 100;
    private int score = 0;
    public HunterBase Hunter { get => hunter; set => hunter = value; }
    public Inventory Inventory { get => inventory; set => inventory = value; }
    public int Money { get => money; set => money = value; }
    public int Score { get => score; set => score = value; }
    public Item CurrentItem { get => currentItem; set => currentItem = value; }

    //private Vector2 movement => (Vector2)cam.transform.position - backgroundOffset;
    //private float playerDistance => transform.position.z - something.position.z;
    //private float clipping => cam.transform.position.z + (playerDistance > 0 ? cam.farClipPlane : cam.nearClipPlane);
    //private float parallax => Mathf.Abs(playerDistance) / clipping;
    public Player() {
        inventory = new Inventory();
    }

    public Player(HunterBase hunter, Inventory inventory, int money, int score)
    {
        this.hunter = hunter;
        this.inventory = inventory;
        this.money = money;
        this.score = score;
    }
    public void SetWeapon()
    {
        inventory.AddItem(hunter.Weapon);
        currentItem = hunter.Weapon;
        hunter.Weapon.SetProjectile();
    }
    public void TakeDamage(int damage)
    {
        hunter.TakeDamage(damage);
    }
    private bool HasItem(Item item)
    {
        return inventory.GetItems().Contains(item);
    }
    public void ShootProjectile(Vector2 cursorPos)
    {
        // cursor x,y
        // onclick
        switch (hunter.Weapon.ItemType)//current weapon
        {
            case ItemType.AngelBlessing:
                break;
            case ItemType.Book:
                break;
            case ItemType.Flamethrower:
                break;
            case ItemType.Machete:
                break;
            case ItemType.Pistol:
                break;
            case ItemType.Shotgun:
                break;
            default:
                break;
        }
    }
    public void SwitchWeapon(WeaponBase weapon)
    {
        if (!HasItem(weapon))
            return;
        switch (hunter.Weapon.ItemType)
        {
            case ItemType.AngelBlessing:
                break;
            case ItemType.Book:
                break;
            case ItemType.Flamethrower:
                break;
            case ItemType.Machete:
                break;
            case ItemType.Pistol:
                break;
            case ItemType.Shotgun:
                break;
            default:
                break;
        }
    }
    public void UsePowerUp(Item item)
    {
        if (!HasItem(item))
            return;
        switch (item.ItemType)
        {
            case ItemType.Attack:
                break;               
            case ItemType.AttackSpeed:   
                break;               
            case ItemType.HitPoint:      
                break;               
            case ItemType.MagicCircle:    
                break;
            case ItemType.MovementSpeed:
                break;
            case ItemType.WeaponLevel:
                break;
            case ItemType.WipeEnemies:
                break;
            default:
                break;
        }
    }
    public void UseItem(Item item)
    {
        // use in item.use
        if (!HasItem(item))
            return;
        switch (item.ItemType)
        {
            case ItemType.AmmoBundle:
                break;
            case ItemType.Bandage:
                break;
            case ItemType.Beer:
                break;
            case ItemType.Burger:
                break;
            case ItemType.Cocktail:
                break;
            case ItemType.Pie:
                break;
            case ItemType.Potion:
                break;
            case ItemType.Salad:
                break;
            case ItemType.Shield:
                break;
            default:
                break;
        }
    }
}
