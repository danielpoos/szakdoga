using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "Scriptable Objects/Mob")]

public class Mob : ScriptableObject
{
    protected Sprite sprite;
    protected int level = 1;
    protected int experience = 0; // hunter => get, monsters => give
    protected int maxHitPoints = 0;
    protected int hitPoints = 0;
    protected int attack = 0;
    protected float attackSpeed = 0f;
    protected float movementSpeed = 100f;
    protected bool isHunter = false;
    protected bool isGameOver = false;
    protected bool isDead = false;
    public Vector2 position = new(0, 0);
    public bool rotation = false; //left or right

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int Level { get => level; set => level = value; }
    public int XP { get => experience; set => experience = value; }
    public int MaxHP { get => maxHitPoints; set => maxHitPoints = value; }
    public int HP { get => hitPoints; set => hitPoints = value; }
    public int Attack { get => attack; set => attack = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsDead { get => isGameOver; set => isGameOver = value; }

    public void TakeDamage(int damage)
    {
        // heal
        int nextHitPoints = hitPoints - damage;
        if (nextHitPoints > 0) hitPoints = nextHitPoints;
        else
        {
            if (isHunter)
            {
                // player die event
                isGameOver = true;
            }
            else
            {
                // destroy instance
                isDead = true;
            }
        }
    }
}
