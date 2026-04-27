using System;
using UnityEngine;

public class MonsterBase : Mob
{
    protected Item itemDrop;
    public Vector2 destination = new(0, 0);
    public Item ItemDrop { get { return itemDrop; } set { itemDrop = value; } }
    public void SetupMonsterBase()
    {
        level = 1;
        experience = 0;
        maxHitPoints = hitPoints = 100;
        attack = 10;
        movementSpeed = 50f;
    }
    public void LevelUpMonster(int level, Difficulty diff)
    {
        switch (diff)
        {
            case Difficulty.Normal:
                experience = level * ((int)diff + 1) + 25;
                maxHitPoints = hitPoints = level*120;
                attack += (int)(level * .5f);
                break;
            case Difficulty.Hard:
                experience = level * ((int)diff + 1) + 15;
                maxHitPoints = hitPoints = level*150;
                attack += (int)(level * 1f);
                break;
            default:
                experience = level * ((int)diff + 1) + 50;
                maxHitPoints = hitPoints = level*100;
                attack += (int)(level * .3f);
                break;
        }
    }
    //give exp, item drop to player
    public virtual Item OnDeath() { return itemDrop; }

    public void SetItemDrop()
    {
        itemDrop = this switch
        {
            Angel => GetRandomDrop(),
            Demon => GetRandomDrop(),
            Leviathan => GetRandomDrop(),
            Shapeshifter => GetRandomDrop(),
            Vampire => GetRandomDrop(),
            Werewolf => GetRandomDrop(),
            _=>null
        };
    }
    private Item GetRandomDrop()
    {
        return (int)(UnityEngine.Random.value * 6) switch
        {
            1 => new Item(ItemType.AmmoBundle),
            2 => new Item(ItemType.Potion),
            3 => new Item(ItemType.MagicCircle),
            4 => new Item(ItemType.Shield),
            5 => new Item(ItemType.WeaponLevel),
            _ => new Item(ItemType.WipeEnemies),
        };
    }
}
