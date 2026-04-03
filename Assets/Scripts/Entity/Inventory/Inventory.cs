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
    public List<Item> GetItems()
    {
        return inventory;
    }
}
