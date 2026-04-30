public class WeaponBase : Item
{
    protected int level;
    protected ProjectileBase projectile;

    public int Level { get => level; set => level = value; }
    public ProjectileBase Projectile { get => projectile; set => projectile = value; }

    public WeaponBase(ItemType itemType) : base(itemType)
    {
        ItemType = itemType;
        level = 1;
        Sprite = GetSprite();
    }
    public WeaponBase(ItemType itemType, int level) : base(itemType)
    {
        ItemType = itemType;
        this.level = level;
        Sprite = GetSprite();
    }
    public void SetDamage(int damage)
    {
        projectile.SetDamage((int)(damage * (quality + level / 100.0)));
    }
    public void SetProjectile()
    {
        switch (ItemType)
        {
            case ItemType.AngelBlade: projectile = new ProjectileBase(ProjectileType.AngelLight); break;
            case ItemType.Book: projectile = new ProjectileBase(ProjectileType.Spell); break;
            case ItemType.Flamethrower: projectile = new ProjectileBase(ProjectileType.Fire); break;
            case ItemType.Machete: projectile = new ProjectileBase(ProjectileType.Slash); break;
            case ItemType.Pistol: projectile = new ProjectileBase(ProjectileType.Bullet); break;
            case ItemType.Shotgun: projectile = new ProjectileBase(ProjectileType.Bullet); break;
        }
    }
    public void SetProjectile(int damage)
    {
        switch (ItemType)
        {
            case ItemType.AngelBlade: projectile = new ProjectileBase(ProjectileType.AngelLight); break;
            case ItemType.Book: projectile = new ProjectileBase(ProjectileType.Spell); break;
            case ItemType.Flamethrower: projectile = new ProjectileBase(ProjectileType.Fire); break;
            case ItemType.Machete: projectile = new ProjectileBase(ProjectileType.Slash); break;
            case ItemType.Pistol: projectile = new ProjectileBase(ProjectileType.Bullet); break;
            case ItemType.Shotgun: projectile = new ProjectileBase(ProjectileType.Bullet); break;
        }
        projectile.Damage = (int)(damage * (quality + level / 100.0));
    }
}
