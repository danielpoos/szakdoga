public class Castiel : HunterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Castiel;
        weapon = new WeaponBase(ItemType.AngelBlade);
    }
}
