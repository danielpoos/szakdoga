using UnityEngine;

public class Leviathan : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Leviathan;
        itemDrop = new(ItemType.Bandage);
        MaxHP = hitPoints = 100;
    }
}
