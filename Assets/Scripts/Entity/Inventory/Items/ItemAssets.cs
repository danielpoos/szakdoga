using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // item sprites
    public Sprite AmmoBundle;
    public Sprite Bandage;
    public Sprite Burger;
    public Sprite Beer;
    public Sprite Cocktail;
    public Sprite Pie;
    public Sprite Potion;
    public Sprite Salad;
    public Sprite Shield;
    public Sprite Whiskey;
    // powerup sprites
    public Sprite Attack;
    public Sprite AttackSpeed;
    public Sprite HitPoint;
    public Sprite MagicCircle;
    public Sprite MovementSpeed;
    public Sprite WeaponLevel;
    public Sprite WipeEnemies;
    // weapon sprites
    public Sprite AngelBlessing;
    public Sprite Book;
    public Sprite Flamethrower;
    public Sprite Machete;
    public Sprite Pistol;
    public Sprite Shotgun;
}
