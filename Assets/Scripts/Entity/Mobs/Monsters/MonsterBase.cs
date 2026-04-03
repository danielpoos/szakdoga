public class MonsterBase : Mob
{
    protected ItemBase itemDrop;
    public ItemBase ItemDrop { get; }
    private void Awake()
    {
        
    }
    public void MoveToPlayerPos()
    {

    }
    public void CollideWithPlayer()
    {

    }
    public void CollideWithProjectile()
    {

    }
    //give exp, item drop to player
    public virtual ItemBase OnDeath() { return itemDrop; }
    public int Calculate_Experience(int round)
    {
        experience = level * (hitPoints + (int)attack);
        return experience / round;
    }
}
