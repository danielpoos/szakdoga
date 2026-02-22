using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private HunterBase hunter; //has position
    private Dictionary<ItemBase, int> inventory; // base weapon
    private int money;
    // background position

    private void Awake()
    {
        //hunter = chosen;
        inventory.Add(hunter.Weapon, 1);
        money = 100;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void SwitchWeapon()
    {

    }
}
