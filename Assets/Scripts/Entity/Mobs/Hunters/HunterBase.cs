using System.Diagnostics;
using UnityEngine.Events;

public class HunterBase : Mob
{
    protected WeaponBase weapon;
    private Difficulty difficulty;
    private Inventory inventory;
    private Item currentItem;
    private int money = 100;
    private int score = 0;
    private bool canGoNextRound = true;
    public UnityEvent LevelUpEvent = new();
    public WeaponBase Weapon { get => weapon; set => weapon = value; }
    public Inventory Inventory { get => inventory; set => inventory = value; }
    public Item CurrentItem { get => currentItem; set => currentItem = value; }
    public Difficulty Difficulty { get => difficulty; set => difficulty = value; }
    public int Money { get => money; set => money = value; }
    public int Score { get => score; set => score = value; }
    public bool CanGoNextRound { get => canGoNextRound; set => canGoNextRound = value; }
    public HunterBase()
    {
        inventory = new Inventory();
        level = 1;
        LevelUpEvent = new();
        LevelUpEvent.AddListener(IncreaseLevel);
        MaxHP = hitPoints = 100;
        attack = 20;
    }
    public void BuyAttack()
    {
        attack += (int)(attack * 0.1f);
    }
    public void BuyHP()
    {
        maxHitPoints += 100;
        hitPoints += 100;
    }
    public void IncreaseLevel()
    {
        level += 1;
        //incerase stats
        attack += (int)(level + 10.5f);
        maxHitPoints += level + 120;
        hitPoints = maxHitPoints;
    }
    public void AddHP(int amount)
    {
        if (HP + amount > maxHitPoints) HP = maxHitPoints;
        else HP += amount;
    }
    public void AddAttack(int amount)
    {
        attack += amount;
    }
    public void AddXP(int xp)
    {
        int xpAdded = experience + xp;
        if (xpAdded >= ExperienceForNextLevel())
        {
            experience = xpAdded - ExperienceForNextLevel();
            LevelUpEvent.Invoke();
            canGoNextRound = true;
        }
        else
        {
            experience = xpAdded;
        }
    }
    public void AddMoney(int amount)
    {
        money += amount;
    }
    public int ExperienceForNextLevel()
    {
        return level * ((int)difficulty + 2) * 20;
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    public void IncreaseMoney(int amount)
    {
        money += amount;
    }
    public bool HasEnoughMoney(int price)
    {
        return money >= price;
    }
    public void SetWeapon()
    {
        if (!inventory.Contains(weapon)) inventory.AddItem(weapon);
        currentItem = weapon;
        weapon.SetProjectile();
    }
    private bool HasItem(Item item)
    {
        return inventory.GetItems().Contains(item);
    }
    public void SwitchForward()
    {
        int invCount = inventory.GetItems().Count;
        if (inventory.GetItems().Count > 1)
        {
            int index = inventory.GetPosition(currentItem);
            if (index < invCount - 1) currentItem = inventory.GetItems()[index + 1];
            else currentItem = inventory.GetItems()[0];
            if (currentItem.GetType() == typeof(WeaponBase))
            {
                SwitchWeapon((WeaponBase)currentItem);
            }
        }
        else
        {
            currentItem = inventory.GetItems()[0];
            SwitchWeapon((WeaponBase)currentItem);
        }
        currentItem.Sprite = currentItem.GetSprite();
    }
    public void SwitchBackward()
    {
        int invCount = inventory.GetItems().Count;
        if (inventory.GetItems().Count > 1)
        {
            int index = inventory.GetPosition(currentItem);
            if (index > 0) currentItem = inventory.GetItems()[index - 1];
            else currentItem = inventory.GetItems()[invCount - 1];
            if (currentItem.GetType() == typeof(WeaponBase))
            {
                SwitchWeapon((WeaponBase)currentItem);
            }
        }
        else
        {
            currentItem = inventory.GetItems()[0];
            SwitchWeapon((WeaponBase)currentItem);
        }
        currentItem.Sprite = currentItem.GetSprite();
    }
    public void SwitchWeapon(WeaponBase weapon)
    {
        if (currentItem == null || !HasItem(weapon))
            return;
        switch (weapon.ItemType)
        {
            case ItemType.AngelBlessing: weapon.SetProjectile(); break;
            case ItemType.Book: weapon.SetProjectile();  break;
            case ItemType.Flamethrower: weapon.SetProjectile();  break;
            case ItemType.Machete: weapon.SetProjectile();  break;
            case ItemType.Pistol: weapon.SetProjectile();  break;
            case ItemType.Shotgun: weapon.SetProjectile();  break;
            default: break;
        }
    }
    public void UseItem()
    {
        if (currentItem == null || !HasItem(currentItem))
            return;
        switch (currentItem.ItemType)
        {
            case ItemType.AmmoBundle: AddAttack(60); break;
            case ItemType.Bandage: AddHP(60); break;
            case ItemType.Beer: AddHP(-10); AddAttack(30); break;
            case ItemType.Burger: AddHP(40); AddAttack(-10); break;
            case ItemType.Cocktail: AddHP(30); AddAttack(30); break;
            case ItemType.MagicCircle: AddAttack(40); break;
            case ItemType.Pie: AddHP(20); AddAttack(40); break;
            case ItemType.Potion: AddAttack(-40); AddHP(-40); break;
            case ItemType.Salad: AddHP(40); AddAttack(10); break;
            case ItemType.Shield: AddHP(40); break;
            case ItemType.WeaponLevel: weapon.Level += 1; break;
            case ItemType.Whiskey: AddAttack(40); AddHP(-20); break;
            case ItemType.WipeEnemies: /*event*/ break;
            default: break;
        }
    }
}
