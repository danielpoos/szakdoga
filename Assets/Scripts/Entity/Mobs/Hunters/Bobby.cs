public class Bobby : HunterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Bobby;
        weapon = new WeaponBase(ItemType.Flamethrower);
    }
}
