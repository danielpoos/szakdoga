using UnityEngine;

public class MonsterBase : Mob
{
    protected Item itemDrop;
    public Vector2 destination = new(0, 0);
    public Item ItemDrop { get; }
    public MonsterBase()
    {
        level = 1;
        experience = 20;
        maxHitPoints = 200;
        hitPoints = 200;
        attack = 10;
        attackSpeed = 3f;
        movementSpeed = 50f;
    }
    public void LevelUpMonster(int level)
    {
        for (int i = 0; i < level; i++)
        {
            experience += level * 2;
            maxHitPoints = hitPoints *= level+1;
            attack *= (int)(level * .2f);
        }
    }
    public void CollideWithPlayer()
    {

    }
    public void CollideWithProjectile()
    {

    }
    //give exp, item drop to player
    public virtual Item OnDeath() { return itemDrop; }
}
