using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryExtended : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemContainer;
    private Transform itemSlotTemplate;
    public int MaxLength = -1;
    public float size = 50f;

    private void Awake()
    {
        itemContainer = transform;
        itemSlotTemplate = itemContainer.Find("ItemTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventory();
    }
    private void RefreshInventory()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "ItemTemplate") continue;
            GameObject.Destroy(child.gameObject);
        }
        if (itemSlotTemplate == null || itemContainer == null) Awake();
        int x = 0, y = 0;
        foreach (Item item in inventory.GetItems())
        {
            RectTransform itemSlot = Instantiate(itemSlotTemplate,itemContainer).GetComponent<RectTransform>();
            itemSlot.gameObject.SetActive(true);
            itemSlot.name = $"ItemSlot{inventory.GetPosition(item)}";
            Transform renderer = itemSlot.Find("Renderer");
            renderer.transform.position = new Vector3(renderer.transform.position.x, renderer.transform.position.y,-1);  
            SpriteRenderer spriteRenderer = renderer.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = item.GetSprite();
            itemSlot.anchoredPosition = new Vector2(x * size, y * size);
            x++;
            if (MaxLength != -1 && x >= MaxLength) break;
            if (x>MaxLength)
            {
                x = 0;
                y++;
            }
        }
    }
}
