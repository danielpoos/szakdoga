using System;
using System.Collections.Generic;

public class Inventory
{
    public List<Item> inventory;

    public Inventory()
    {
        this.inventory = new List<Item>();
    }
    public void AddItem(Item item)
    {
        inventory.Add(item);
    }
    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }
    public List<Item> GetItems()
    {
        return inventory;
    }
    public int GetPosition(Item item)
    {
        return inventory.IndexOf(item);
    }
}
