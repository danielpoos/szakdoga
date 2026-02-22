using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]

public class ItemBase : ScriptableObject
{
    //picture asset
    private int quantity = 0;
    private int quality = 0;
    private bool stackable = false;
    private bool instantUse = false;
    private float activateTime = 0;
    private float attackIncrease = 0;
    private float hitPointIncrease = 0;
    private int weaponLevelIncrease = 0;
    private bool isPowerUp = false; // can be picked up

    protected int Quantity { get => quantity; set => quantity = value; }
    protected int Quality { get => quality; set => quality = value; }
}
