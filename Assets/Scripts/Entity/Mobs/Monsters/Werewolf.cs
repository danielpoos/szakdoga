public class Werewolf : MonsterBase
{
    private void Awake()
    {
        SetupMonsterBase();
        sprite = CharacterAssets.Instance.Werewolf;
        itemDrop = new(ItemType.Bandage);
    }
}
