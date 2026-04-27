using UnityEngine;
public enum ProjectileType
{
    AngelLight,
    Bullet,
    Fire,
    Slash,
    Spell
}
public class ProjectileBase
{
    protected ProjectileType projectileType;
    protected Sprite sprite;
    protected int damage = 20;
    protected float movementSpeed = 120f;
    protected Vector2 destination;
    public int Damage { get => damage; set => damage = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public Vector2 Destination { get => destination; set => destination = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }

    public ProjectileBase(ProjectileType projectileType)
    {
        this.projectileType = projectileType;
        this.sprite = GetSprite();
    }
    public void SetDamage(int amount)
    {
        damage = amount;
    }
    public Sprite GetSprite()
    {
        return projectileType switch
        {
            ProjectileType.AngelLight => ProjectileAssets.Instance.AngelLight,
            ProjectileType.Bullet => ProjectileAssets.Instance.Bullet,
            ProjectileType.Fire => ProjectileAssets.Instance.Fire,
            ProjectileType.Slash => ProjectileAssets.Instance.Slash,
            ProjectileType.Spell => ProjectileAssets.Instance.Spell,
            _ => ProjectileAssets.Instance.Bullet,
        };
    }
}
