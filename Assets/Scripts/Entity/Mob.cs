using UnityEngine;

public class Mob : ScriptableObject
{
    protected int level = 0;
    protected int experience = 0;
    protected int hitpoints = 0;
    protected float attack = 0f; // set base to each
    protected float attackspeed = 0f;
    protected float movementspeed = 0f;
    protected bool isHunter = false;
    [SerializeField] public Vector2 position;
    [SerializeField] public Vector2 destination;
    [SerializeField] public Quaternion rotation;
    protected int Level { get => level; set => level = value; }
    protected int XP { get => experience; set => experience = value; }
    protected int HP { get => hitpoints; set => hitpoints = value; }
    protected bool IsHunter { get => isHunter; set => isHunter = value; }
    protected void IncreaseLevel()
    {
        int newLevel = this.experience * this.level;
        this.level = newLevel;
        this.attack *= this.level / 10.0f;
    }
}
