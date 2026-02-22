using UnityEngine;

public class WeaponBase : ItemBase
{
    protected int level;
    protected int experience;
    protected float durability;
    private void Awake()
    {
        Quality = 1;
        Quantity = 1;
    }
}
