using UnityEngine;

public enum ItemType
{
    // items
    AmmoBundle,
    Bandage,
    Beer,
    Burger,
    Cocktail,
    MagicCircle,
    Pie,
    Potion,
    Salad,
    Shield,
    Whiskey,
    WipeEnemies,
    // powerups
    Attack,
    AttackSpeed,
    MovementSpeed,
    WeaponLevel,
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
    protected ItemType itemType;
    protected Sprite sprite;
    protected int quantity;
    protected int quality;
    public Vector2 position;
    public Item(ItemType itemType, int quality = 1, int quantity = 1)
    {
        this.itemType = itemType;
        this.quality = quality;
        this.quantity = quantity;
    }
    public ItemType ItemType { get => itemType; set => itemType = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public int Quality { get => quality; set => quality = value; }
    public Sprite GetSprite()
    {
        return ItemType switch
        {
            ItemType.AmmoBundle => ItemAssets.Instance.AmmoBundle,
            ItemType.Bandage => ItemAssets.Instance.Bandage,
            ItemType.Beer => ItemAssets.Instance.Beer,
            ItemType.Burger => ItemAssets.Instance.Burger,
            ItemType.Cocktail => ItemAssets.Instance.Cocktail,
            ItemType.MagicCircle => ItemAssets.Instance.MagicCircle,
            ItemType.Pie => ItemAssets.Instance.Pie,
            ItemType.Potion => ItemAssets.Instance.Potion,
            ItemType.Salad => ItemAssets.Instance.Salad,
            ItemType.Shield => ItemAssets.Instance.Shield,
            ItemType.WeaponLevel => ItemAssets.Instance.WeaponLevel,
            ItemType.Whiskey => ItemAssets.Instance.Whiskey,
            ItemType.WipeEnemies => ItemAssets.Instance.WipeEnemies,

            ItemType.AngelBlessing => ItemAssets.Instance.AngelBlessing,
            ItemType.Book => ItemAssets.Instance.Book,
            ItemType.Flamethrower => ItemAssets.Instance.Flamethrower,
            ItemType.Machete => ItemAssets.Instance.Machete,
            ItemType.Pistol => ItemAssets.Instance.Pistol,
            ItemType.Shotgun => ItemAssets.Instance.Shotgun,
            _ => ItemAssets.Instance.Shield,
        };
    }
}