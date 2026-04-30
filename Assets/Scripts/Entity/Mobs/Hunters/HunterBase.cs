using UnityEngine;
using UnityEngine.Events;

public class HunterBase : Mob
{
    protected WeaponBase weapon;
    private Difficulty difficulty;
    private Inventory inventory;
    private Item currentItem;
    private int money = 100;
    private int score = 0;
    private int next = -1;
    private bool canGoNextRound = true;
    public UnityEvent LevelUpEvent = new();
    public WeaponBase Weapon { get => weapon; set => weapon = value; }
    public Inventory Inventory { get => inventory; set => inventory = value; }
    public Item CurrentItem { get => currentItem; set => currentItem = value; }
    public Difficulty Difficulty { get => difficulty; set => difficulty = value; }
    public int Money { get => money; set => money = value; }
    public int Score { get => score; set => score = value; }
    public int Next { get => next; set => next = value; }
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
    //public void BuyAttack()
    //{
    //    attack += (int)(attack * 0.1f);
    //}
    //public void BuyHP()
    //{
    //    maxHitPoints += 100;
    //    hitPoints += 100;
    //}
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
        if (amount > 0)
        {
            amount += amount * (currentItem.Quality / 100);
            if (HP + amount > maxHitPoints) HP = maxHitPoints;
            else HP += amount;
        }
        else TakeDamage(-amount);
    }
    public void AddAttack(int amount)
    {
        amount += amount * (currentItem.Quality / 100);
        attack += amount;
        weapon.SetDamage(attack);
    }
    public void AddXP(int xp)
    {
        int xpAdded = experience + xp;
        if (xpAdded >= ExperienceForNextLevel())
        {
            experience = xpAdded - ExperienceForNextLevel();
            weapon.SetDamage(attack);
            weapon.SetProjectile(attack);
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
    public bool HasEnoughMoney(int price)
    {
        return money >= price;
    }
    public void SetWeapon()
    {
        if (!inventory.Contains(weapon)) inventory.AddItem(weapon);
        currentItem = weapon;
        weapon.SetProjectile(attack);
    }
    private bool HasItem(Item item)
    {
        return inventory.GetItems().Contains(item);
    }
    public void SwitchForward()
    {
        int invCount = inventory.GetItems().Count;
        if (invCount > 1)
        {
            int index;
            if (inventory.GetPosition(currentItem) < 0 && next != -1) index = next-1;
            else index = inventory.GetPosition(currentItem);
            if (index < invCount - 1) { currentItem = inventory.GetItems()[index + 1]; }
            else { currentItem = inventory.GetItems()[0]; }
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

            int index;
            if (inventory.GetPosition(currentItem) < 0 && next != -1) index = next;
            else index = inventory.GetPosition(currentItem);
            if (index > 0) { currentItem = inventory.GetItems()[index - 1]; }
            else { currentItem = inventory.GetItems()[invCount - 1]; }
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
    public void SelectCurrentItem()
    {
        for (int i = 0; i < inventory.GetItems().Count; i++)
        {
            if (inventory.GetItems()[i].GetType() == typeof(WeaponBase))
            {
                currentItem = inventory.GetItems()[i];
                SwitchWeapon((WeaponBase)currentItem);
            }
        }
        currentItem ??= inventory.GetItems()[0];
        currentItem.Sprite = currentItem.GetSprite();
    }
    public void SwitchWeapon(WeaponBase weapon)
    {
        if (currentItem == null || !HasItem(weapon) || currentItem.GetType() != typeof(WeaponBase))
            return;
        switch (weapon.ItemType)
        {
            case ItemType.AngelBlade: weapon.SetProjectile(); break;
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
            case ItemType.AmmoBundle: AddAttack(50); AddHP(-10); break;
            case ItemType.Bandage: AddHP(60); break;
            case ItemType.Beer: AddHP(20); AddAttack(30); break;
            case ItemType.Burger: AddHP(10); AddAttack(40); break;
            case ItemType.Cocktail: AddHP(30); AddAttack(20); break;
            case ItemType.MagicCircle: AddAttack(40); AddHP(-20); break;
            case ItemType.Pie: AddHP(25); AddAttack(25); break;
            case ItemType.Potion: AddAttack(-40); AddHP(-40); break;
            case ItemType.Salad: AddHP(40); AddAttack(10); break;
            case ItemType.Shield: AddHP(50); break;
            case ItemType.WeaponLevel: weapon.Level += 1; break;
            case ItemType.Whiskey: AddAttack(40); AddHP(20); break;
            case ItemType.WipeEnemies: AddAttack(-20); AddHP(-50); break;
            default: break;
        }
    }
}
