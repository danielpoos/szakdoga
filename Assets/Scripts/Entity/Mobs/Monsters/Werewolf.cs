using UnityEngine;

public class Werewolf : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Werewolf;
        itemDrop = new(ItemType.Bandage);
        MaxHP = hitPoints = 100;
    }
}
