using UnityEngine;

public class ProjectileAssets : MonoBehaviour
{
    public static ProjectileAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // projectile sprites
    public Sprite AngelLight;
    public Sprite Bullet;
    public Sprite Fire;
    public Sprite Slash;
    public Sprite Spell;
}