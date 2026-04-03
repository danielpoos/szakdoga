using UnityEngine;

public class Angel : MonsterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Angel;
    }
}
