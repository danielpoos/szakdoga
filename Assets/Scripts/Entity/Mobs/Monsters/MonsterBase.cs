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
    public void LevelUpMonster(int level)
    {
        experience = level * 15;
        maxHitPoints = hitPoints += level*100;
        attack += (int)(level * .3f);
    }
    //give exp, item drop to player
    public virtual Item OnDeath() { return itemDrop; }

    public void SetItemDrop()
    {
        itemDrop = this switch
        {
            Angel => new Item(ItemType.AmmoBundle),
            Demon => new Item(ItemType.Cocktail),
            Leviathan => new Item(ItemType.MagicCircle),
            Shapeshifter => new Item(ItemType.Shield),
            Vampire => new Item(ItemType.WeaponLevel),
            Werewolf => new Item(ItemType.WipeEnemies),
            _=>new Item(ItemType.Pie)
            //ItemType.AmmoBundle => ItemAssets.Instance.AmmoBundle,
            //ItemType.Bandage => ItemAssets.Instance.Bandage,
            //ItemType.Beer => ItemAssets.Instance.Beer,
            //ItemType.Burger => ItemAssets.Instance.Burger,
            //ItemType.Cocktail => ItemAssets.Instance.Cocktail,
            //ItemType.MagicCircle => ItemAssets.Instance.MagicCircle,
            //ItemType.Pie => ItemAssets.Instance.Pie,
            //ItemType.Potion => ItemAssets.Instance.Potion,
            //ItemType.Salad => ItemAssets.Instance.Salad,
            //ItemType.Shield => ItemAssets.Instance.Shield,
            //ItemType.WeaponLevel => ItemAssets.Instance.WeaponLevel,
            //ItemType.Whiskey => ItemAssets.Instance.Whiskey,
            //ItemType.WipeEnemies => ItemAssets.Instance.WipeEnemies,
        };
    }

}
