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
    protected int damage = 0;
    protected int range = 100;
    protected float movementSpeed = 20f;
    // no need ???
    protected Vector2 position;
    protected Vector2 destination;
    protected Quaternion rotation;
    // ^^
    public int Damage { get => damage; set => damage = value; }
    public int Range { get => range; set => range = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public Vector2 Position { get => position; set => position = value; }
    public Vector2 Destination { get => destination; set => destination = value; }
    public Quaternion Rotation { get => rotation; set => rotation = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }

    public ProjectileBase(ProjectileType projectileType)
    {
        this.projectileType = projectileType;
        this.sprite = GetSprite();
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
    public void Move(Vector2 toPos, Vector2 fromPos)
    // move from current player position
    {
        // if opponent hit then destroy
        // else if pos == dest
    }
}
