using UnityEngine;

public class Vampire : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Vampire;
        itemDrop = new(ItemType.Bandage);
        MaxHP = hitPoints = 100;
    }
}
