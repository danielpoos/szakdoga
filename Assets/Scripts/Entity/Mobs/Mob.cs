using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "Scriptable Objects/Mob")]

public class Mob : ScriptableObject
{
    protected Sprite sprite;
    protected int level = 1;
    protected int experience = 0;
    protected int maxHitPoints = 0;
    protected int hitPoints = 0;
    protected int attack = 0;
    protected float movementSpeed = 100f;
    protected bool isDead = false;
    public Vector2 position = new(0, 0);
    public bool rotation = false; //left or right

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int Level { get => level; set => level = value; }
    public int XP { get => experience; set => experience = value; }
    public int MaxHP { get => maxHitPoints; set => maxHitPoints = value; }
    public int HP { get => hitPoints; set => hitPoints = value; }
    public int Attack { get => attack; set => attack = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    public void TakeDamage(int damage)
    {
        int nextHitPoints = hitPoints - damage;
        if (nextHitPoints > 0) hitPoints = nextHitPoints;
        else
        {
            hitPoints = 0;
            isDead = true;
        }
    }
}
