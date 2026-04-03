public class WeaponBase : Item
{
    protected int level;
    protected float durability;
    protected ProjectileBase projectile;
    public WeaponBase(ItemType itemType)
    {
        ItemType = itemType;
        level = 1;
        durability = 100f;
    }
    public WeaponBase(ItemType itemType, int level, float durability)
    {
        ItemType = itemType;
        this.level = level;
        this.durability = durability;
        Sprite = GetSprite();
    }
    public void SetProjectile()
    {
        switch (ItemType)
        {
            case ItemType.AngelBlessing: projectile = new ProjectileBase(ProjectileType.AngelLight); break;
            case ItemType.Book: projectile = new ProjectileBase(ProjectileType.Spell); break;
            case ItemType.Flamethrower: projectile = new ProjectileBase(ProjectileType.Fire); break;
            case ItemType.Machete: projectile = new ProjectileBase(ProjectileType.Slash); break;
            case ItemType.Pistol: projectile = new ProjectileBase(ProjectileType.Bullet); break;
            case ItemType.Shotgun: projectile = new ProjectileBase(ProjectileType.Bullet); break;
        }
    }
}
