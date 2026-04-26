using UnityEngine;

public class Vampire : MonsterBase
{
    private void Awake()
    {
        SetupMonsterBase();
        sprite = CharacterAssets.Instance.Vampire;
        itemDrop = new(ItemType.Bandage);
    }
}
