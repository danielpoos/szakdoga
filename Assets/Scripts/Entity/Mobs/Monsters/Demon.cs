using UnityEngine;

public class Demon : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Demon;
    }
}
