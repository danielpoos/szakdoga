public class Leviathan : MonsterBase
{
    private void Awake()
    {
        SetupMonsterBase();
        sprite = CharacterAssets.Instance.Leviathan;
        itemDrop = new(ItemType.Bandage);
    }
}
