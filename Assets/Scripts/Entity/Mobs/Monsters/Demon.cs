using UnityEngine;

public class Demon : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Demon;
        itemDrop = new(ItemType.Bandage);
        MaxHP = hitPoints = 100;
    }
}
