public class PowerUpBase : Item
{
    protected float useTime = 0;
    protected bool stackable = false;
    protected bool instantUse = false;
    protected float activateTime = 0;
    protected float attackChange = 0;
    protected float attackSpeedChange = 0;
    protected float movementSpeedChange = 0;
    protected float hitPointChange = 0;
    protected int weaponLevelChange = 0;
    protected float shieldHitPoints = 0;

    public float UseTime { get => useTime; set => useTime = value; }
    public bool Stackable { get => stackable; set => stackable = value; }
    public bool InstantUse { get => instantUse; set => instantUse = value; }
    public float ActivateTime { get => activateTime; set => activateTime = value; }
    public float AttackChange { get => attackChange; set => attackChange = value; }
    public float AttackSpeedChange { get => attackSpeedChange; set => attackSpeedChange = value; }
    public float MovementSpeedChange { get => movementSpeedChange; set => movementSpeedChange = value; }
    public float HitPointChange { get => hitPointChange; set => hitPointChange = value; }
    public float ShieldHitPoints { get => shieldHitPoints; set => shieldHitPoints = value; }
    public int WeaponLevelChange { get => weaponLevelChange; set => weaponLevelChange = value; }

    public PowerUpBase(ItemType itemType) : base(itemType)
    {
        ItemType = itemType;
        Sprite = GetSprite();
        Quality = quality;
        Quantity = quantity;
        // while (time) { wait } set back value to original
    }
}
