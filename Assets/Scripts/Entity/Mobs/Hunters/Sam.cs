public class Sam : HunterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Sam;
        weapon = new WeaponBase(ItemType.Machete);
    }
}
