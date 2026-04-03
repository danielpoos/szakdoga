public class Rowena : HunterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Rowena;
        weapon = new WeaponBase(ItemType.Book);
    }
}
