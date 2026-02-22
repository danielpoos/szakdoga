using UnityEngine;

public class HunterBase : Mob
{
    protected ItemBase weapon;
    public ItemBase Weapon { get; }
    private void Awake()
    {
        isHunter = true;
    }
}
