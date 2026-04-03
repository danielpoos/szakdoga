using UnityEngine;

public class Werewolf : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Werewolf;
    }
}
