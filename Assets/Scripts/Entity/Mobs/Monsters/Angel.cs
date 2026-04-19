using UnityEngine;

public class Angel : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Angel;
        itemDrop = new(ItemType.Bandage);
        MaxHP = hitPoints = 100;
    }
}
