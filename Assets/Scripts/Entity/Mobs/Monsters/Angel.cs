using UnityEngine;

public class Angel : MonsterBase
{
    private void Awake()
    {
        SetupMonsterBase();
        sprite = CharacterAssets.Instance.Angel;
        itemDrop = new(ItemType.Bandage);
    }
}
