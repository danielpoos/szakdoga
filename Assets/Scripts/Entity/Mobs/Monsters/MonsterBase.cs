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
                maxHitPoints = hitPoints = (level + (int)diff + 1) * 120;
                attack = level *  10;
                break;
            case Difficulty.Hard:
                experience = level * ((int)diff + 1) + 15;
                maxHitPoints = hitPoints = (level + (int)diff + 3) *150;
                attack = level * 15;
                break;
            default:
                experience = level * ((int)diff + 1) + 50;
                maxHitPoints = hitPoints = level*100;
                attack = level * 5;
                break;
        }
    }

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
