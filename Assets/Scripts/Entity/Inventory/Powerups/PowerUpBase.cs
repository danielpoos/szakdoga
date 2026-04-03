public class PowerUpBase : Item
{
    private int quantity = 0;
    private int quality = 0;
    private float useTime = 0;
    private bool stackable = false;
    private bool instantUse = false;
    private float activateTime = 0;
    private float attackChange = 0;
    private float attackSpeedChange = 0;
    private float movementSpeedChange = 0;
    private float hitPointChange = 0;
    private int weaponLevelChange = 0;
    private float shieldHitPoints = 0;

    protected int Quantity { get => quantity; set => quantity = value; }
    protected int Quality { get => quality; set => quality = value; }
    protected float UseTime { get => useTime; set => useTime = value; }
    protected bool Stackable { get => stackable; set => stackable = value; }
    protected bool InstantUse { get => instantUse; set => instantUse = value; }
    protected float ActivateTime { get => activateTime; set => activateTime = value; }
    protected float AttackChange { get => attackChange; set => attackChange = value; }
    protected float AttackSpeedChange { get => attackSpeedChange; set => attackSpeedChange = value; }
    protected float MovementSpeedChange { get => movementSpeedChange; set => movementSpeedChange = value; }
    protected float HitPointChange { get => hitPointChange; set => hitPointChange = value; }
    protected float ShieldHitPoints { get => shieldHitPoints; set => shieldHitPoints = value; }
    protected int WeaponLevelChange { get => weaponLevelChange; set => weaponLevelChange = value; }

    public PowerUpBase(ItemType itemType)
    {
        ItemType = itemType;
        //Sprite = GetSprite();

        // while (time) { wait } set back value to original
    }
}
