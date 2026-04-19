using UnityEngine;

public class Shapeshifter : MonsterBase
{
    private void Awake()
    {
        //red outline??
        sprite = (int)(UnityEngine.Random.value * 6) switch
        {
            1 => CharacterAssets.Instance.Castiel,
            2 => CharacterAssets.Instance.Dean,
            3 => CharacterAssets.Instance.Jody,
            4 => CharacterAssets.Instance.Rowena,
            5 => CharacterAssets.Instance.Sam,
            _ => CharacterAssets.Instance.Bobby,
        };
        itemDrop = new(ItemType.Bandage);
        MaxHP = hitPoints = 100;
    }
}
