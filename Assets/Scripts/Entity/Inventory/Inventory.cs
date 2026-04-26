using System;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> inventory;

    public Inventory()
    {
        inventory = new List<Item>();
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
    public bool Contains(Item item)
    {
        foreach (Item it in inventory)
        {
            if (item.ItemType == it.ItemType) return true;
        }
        return false;
    }
}
