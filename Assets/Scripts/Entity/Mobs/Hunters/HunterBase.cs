public class HunterBase : Mob
{
    protected WeaponBase weapon;
    public WeaponBase Weapon { get => weapon; set => weapon = value; }
    private void Awake()
    {
        isHunter = true;
    }
    protected void IncreaseLevel()
    {
        int newLevel = experience * level;
        level = newLevel;
        attack *= level / 10.0f;
        hitPoints *= (int)(level / 10.0f);
    }
}
