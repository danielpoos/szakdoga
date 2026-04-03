public class Jody : HunterBase
{
    private void Awake()
    {
        sprite = CharacterAssets.Instance.Jody;
        weapon = new WeaponBase(ItemType.Shotgun);
    }
}
