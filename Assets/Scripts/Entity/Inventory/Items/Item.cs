using UnityEngine;

public enum ItemType
{
    // items
    AmmoBundle,
    Bandage,
    Burger,
    Beer,
    Cocktail,
    Pie,
    Potion,
    Salad,
    Shield,
    Whiskey,
    // powerups
    Attack,
    AttackSpeed,
    HitPoint,
    MagicCircle,
    MovementSpeed,
    WeaponLevel,
    WipeEnemies,
    // weapons
    AngelBlessing,
    Book,
    Flamethrower,
    Machete,
    Pistol,
    Shotgun,
}
public class Item
{
    private ItemType itemType;
    private Sprite sprite;

    public ItemType ItemType { get => itemType; set => itemType = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public Sprite GetSprite()
    {
        switch (ItemType)
        {
            case ItemType.AmmoBundle: return ItemAssets.Instance.AmmoBundle;
            case ItemType.Bandage: return ItemAssets.Instance.Bandage;
            case ItemType.Beer: return ItemAssets.Instance.Beer;
            case ItemType.Burger: return ItemAssets.Instance.Burger;
            case ItemType.Cocktail: return ItemAssets.Instance.Cocktail;
            case ItemType.Pie: return ItemAssets.Instance.Pie;
            case ItemType.Potion: return ItemAssets.Instance.Potion;
            case ItemType.Salad: return ItemAssets.Instance.Salad;
            case ItemType.Shield: return ItemAssets.Instance.Shield;
            case ItemType.Whiskey: return ItemAssets.Instance.Whiskey;

            case ItemType.Attack: return ItemAssets.Instance.Attack;
            case ItemType.AttackSpeed: return ItemAssets.Instance.AttackSpeed;
            case ItemType.HitPoint: return ItemAssets.Instance.HitPoint;
            case ItemType.MagicCircle: return ItemAssets.Instance.MagicCircle;
            case ItemType.MovementSpeed: return ItemAssets.Instance.MovementSpeed;
            case ItemType.WeaponLevel: return ItemAssets.Instance.WeaponLevel;
            case ItemType.WipeEnemies: return ItemAssets.Instance.WipeEnemies;
            
            case ItemType.AngelBlessing: return ItemAssets.Instance.AngelBlessing;
            case ItemType.Book: return ItemAssets.Instance.Book;
            case ItemType.Flamethrower: return ItemAssets.Instance.Flamethrower;
            case ItemType.Machete: return ItemAssets.Instance.Machete;
            case ItemType.Pistol: return ItemAssets.Instance.Pistol;
            case ItemType.Shotgun: return ItemAssets.Instance.Shotgun;
            default: return ItemAssets.Instance.Shield;
        };
    }
}