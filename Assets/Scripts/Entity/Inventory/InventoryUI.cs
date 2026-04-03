using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemContainer = transform;
        itemSlotTemplate = itemContainer.Find("ItemSlot");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventory();
    }
    private void RefreshInventory()
    {
        Debug.Log(inventory);
        Debug.Log(itemSlotTemplate);
        Debug.Log(itemContainer);
        foreach (Item item in inventory.GetItems())
        {
            RectTransform itemSlot = Instantiate(itemSlotTemplate,itemContainer).GetComponent<RectTransform>();
            itemSlot.gameObject.SetActive(true);
            Image image = itemSlot.Find("Image").GetComponent<Image>();
            Debug.Log(itemSlot);
            Debug.Log(image);
            image.sprite = item.GetSprite();
        }
    }
}
