using UnityEngine;

public class Vampire : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Vampire;
    }
}
