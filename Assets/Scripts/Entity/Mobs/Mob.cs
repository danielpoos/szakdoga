using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "Scriptable Objects/Mob")]

public class Mob : ScriptableObject
{
    protected Sprite sprite;
    protected int level = 0;
    protected int experience = 0;
    protected int hitPoints = 0;
    protected float attack = 0f; // set base to each
    protected float attackSpeed = 0f;
    private float movementSpeed = 50f;
    protected bool isHunter = false;
    public Vector2 position = new Vector2(0,0);
    public Vector2 destination = new Vector2(0,0);
    public bool rotation = true; //left or right

    public Sprite Sprite { get => sprite; set => sprite = value; }
    protected int Level { get => level; set => level = value; }
    protected int XP { get => experience; set => experience = value; }
    protected int HP { get => hitPoints; set => hitPoints = value; }
    protected bool IsHunter { get => isHunter; set => isHunter = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    public void TakeDamage(int damage)
    {
        // heal
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            if (isHunter)
            {
                // player die event -> endgame
            }
            // destroy instance
            // add points to player
        }
    }
}
