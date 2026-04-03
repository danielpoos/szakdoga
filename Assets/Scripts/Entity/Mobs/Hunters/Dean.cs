public class Dean : HunterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Dean;
        weapon = new WeaponBase(ItemType.Pistol);
    }
}
