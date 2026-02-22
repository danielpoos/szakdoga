using UnityEngine;

public class MonsterBase : Mob
{
    protected ItemBase itemDrop;
    public ItemBase ItemDrop { get; }
    private void Awake()
    {
        
    }
    //give exp, item drop to player
    public virtual ItemBase OnDeath() { return itemDrop; }
    public virtual int Calculate_Experience()
    {
        //experience = level * difficulty * tipus * hp
        return experience;
    }
}
