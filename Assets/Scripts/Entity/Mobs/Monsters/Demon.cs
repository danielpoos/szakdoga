using UnityEngine;

public class Demon : MonsterBase
{
    private void Awake()
    {
        SetupMonsterBase();
        sprite = CharacterAssets.Instance.Demon;
        itemDrop = new(ItemType.Bandage);
    }
}
