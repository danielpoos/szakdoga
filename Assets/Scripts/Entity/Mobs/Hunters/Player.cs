using UnityEngine;

public class Player : MonoBehaviour
{
    private HunterBase hunter;
    private Inventory inventory;
    private Item currentItem;
    private Difficulty difficulty;
    private int money = 100;
    private int score = 0;
    private bool canGoNextRound = true;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public HunterBase Hunter { get => hunter; set => hunter = value; }
    public Inventory Inventory { get => inventory; set => inventory = value; }
    public Item CurrentItem { get => currentItem; set => currentItem = value; }
    public Difficulty Difficulty { get => difficulty; set => difficulty = value; }
    public int Money { get => money; set => money = value; }
    public int Score { get => score; set => score = value; }
    public bool CanGoNextRound { get => canGoNextRound; set => canGoNextRound = value; }

    //private Vector2 movement => (Vector2)cam.transform.position - backgroundOffset;
    //private float playerDistance => transform.position.z - something.position.z;
    //private float clipping => cam.transform.position.z + (playerDistance > 0 ? cam.farClipPlane : cam.nearClipPlane);
    //private float parallax => Mathf.Abs(playerDistance) / clipping;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetPlayer(Difficulty difficulty = Difficulty.Easy) {
        inventory = new Inventory();
        this.difficulty = difficulty;
    }
    public void SetPlayer(Difficulty difficulty, HunterBase hunter, Inventory inventory, int money, int score)
    {
        this.difficulty = difficulty;
        this.hunter = hunter;
        this.inventory = inventory;
        this.money = money;
        this.score = score;
    }
    public void AddXP(int xp)
    {
        int xpAdded = hunter.XP + xp;
        if (xpAdded >= ExperienceForNextLevel())
        {
            hunter.XP = xpAdded - ExperienceForNextLevel();
            hunter.LevelUpEvent.Invoke();
            canGoNextRound = true;
        }
        else
        {
            hunter.XP = xpAdded;
        }
    }
    private int ExperienceForNextLevel()
    {
        return hunter.Level * ((int)difficulty + 1) * (int)(hunter.HP+hunter.Attack) / 11;
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    public void IncreaseMoney(int amount)
    {
        money += amount;
    }
    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void SetWeapon()
    {
        inventory.AddItem(hunter.Weapon);
        currentItem = hunter.Weapon;
        hunter.Weapon.SetProjectile();
        currentItem = inventory.GetItems()[0];
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
    private bool HasItem(Item item)
    {
        return inventory.GetItems().Contains(item);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetType() == typeof(MonsterObject)) TakeDamage(other.GetComponent<MonsterObject>().GetAttack());
    }
    public void SwitchWeapon(WeaponBase weapon)
    {
        if (!HasItem(weapon))
            return;
        switch (hunter.Weapon.ItemType)
        {
            case ItemType.AngelBlessing: break;
            case ItemType.Book: break;
            case ItemType.Flamethrower: break;
            case ItemType.Machete: break;
            case ItemType.Pistol: break;
            case ItemType.Shotgun: break;
            default: break;
        }
    }
    public float UsePowerUp(Item item, bool used, float change = 0)
    {
        //deltatime
        if (!HasItem(item))
            return 0;
        if (!used)
            switch (item.ItemType)
            {
                case ItemType.Attack: change = hunter.Attack * .2f; hunter.Attack += (int)change; break;
                case ItemType.AttackSpeed: change = hunter.AttackSpeed * .2f; hunter.AttackSpeed += change; break;
                case ItemType.MovementSpeed: change = hunter.AttackSpeed * .2f; hunter.MovementSpeed += change; break;
                case ItemType.WeaponLevel: hunter.Weapon.Level += 1; break;
                default: break;
            }
        else
            switch (item.ItemType)
            {
                case ItemType.Attack: hunter.Attack -= (int)change; break;
                case ItemType.AttackSpeed: hunter.AttackSpeed -= change ; break;
                case ItemType.MovementSpeed: hunter.AttackSpeed -= change ; break;
                case ItemType.WeaponLevel: hunter.Weapon.Level -= 1; break;
                default: break;
            }
        return change;
    }
    public void UseItem(Item item)
    {
        // use in item.use
        if (!HasItem(item))
            return;
        switch (item.ItemType)
        {
            case ItemType.AmmoBundle: break;
            case ItemType.Bandage: break;
            case ItemType.Beer: break;
            case ItemType.Burger: break;
            case ItemType.Cocktail: break;
            case ItemType.MagicCircle: break;
            case ItemType.Pie: break;
            case ItemType.Potion: break;
            case ItemType.Salad: break;
            case ItemType.Shield: break;
            case ItemType.WipeEnemies: break;
            default: break;
        }
    }
}
