using UnityEngine.Events;

public class HunterBase : Mob
{
    protected WeaponBase weapon;
    public UnityEvent LevelUpEvent;
    public WeaponBase Weapon { get => weapon; set => weapon = value; }
    public HunterBase()
    {
        isHunter = true;
        LevelUpEvent = new();
        LevelUpEvent.AddListener(IncreaseLevel);
        MaxHP = hitPoints = 100;
        attack = 20;
        //attackSpeed
    }
    public void BuyAttack()
    {
        attack += (int)(attack * 0.1f);
    }
    public void BuyHP()
    {
        hitPoints += 100;
    }
    public void IncreaseLevel()
    {
        level += 1;
        //incerase stats
        attack += (int)(level + 10.5f);
        hitPoints += level + 120;
    }
    public void GetXP(int amount)
    {
        XP += amount;
    }
}
