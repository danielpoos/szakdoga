using UnityEngine;

public class Shapeshifter : MonsterBase
{
    private void Awake()
    {
        switch ((int)UnityEngine.Random.value * 6)
        {
            case 0: sprite = CharacterAssets.Instance.Bobby; break;
            case 1: sprite = CharacterAssets.Instance.Castiel; break;
            case 2: sprite = CharacterAssets.Instance.Dean; break;
            case 3: sprite = CharacterAssets.Instance.Jody; break;
            case 4: sprite = CharacterAssets.Instance.Rowena; break;
            case 5: sprite = CharacterAssets.Instance.Sam; break;
        }

    }
}
