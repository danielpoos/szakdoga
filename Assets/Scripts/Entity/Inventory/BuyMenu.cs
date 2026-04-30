using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyMenu : MonoBehaviour
{
    private Transform itemContainer;
    private Transform itemSlotTemplate;
    public Item[] items = new Item[6];
    public int[] price = new int[6];
    public static UnityEvent<int> ButtonClick = new();
    private GameObject selected;

    private void Awake()
    {
        itemContainer = transform.Find("Container");
        itemSlotTemplate = transform.Find("ItemSlot");
        ButtonClick.RemoveAllListeners();
    }
    public void GenerateItems(int level, int diff)
    {
        for (int i=0; i < 6; i++)
        {
            items[i] = (int)(UnityEngine.Random.value * 9) switch
            {
                0 => new Item(ItemType.Bandage),
                1 => new Item(ItemType.Beer),
                2 => new Item(ItemType.Burger),
                3 => new Item(ItemType.Cocktail),
                4 => new Item(ItemType.Pie),
                5 => new Item(ItemType.Potion),
                6 => new Item(ItemType.Salad),
                7 => new Item(ItemType.Whiskey),
                _ => new Item(ItemType.Shield)
            };
            price[i] = ((int)(UnityEngine.Random.value * 50) * level * diff)+50;
        }
        RefreshInventory();
    }
    private void RefreshInventory()
    {
        foreach (Transform child in itemContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i=0; i< items.Length; i++)
        {
            ButtonClick.RemoveAllListeners();
            RectTransform itemSlot = Instantiate(itemSlotTemplate, itemContainer).GetComponent<RectTransform>();
            itemSlot.gameObject.SetActive(true);
            Transform renderer = itemSlot.Find("Renderer");
            SpriteRenderer spriteRenderer = renderer.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = items[i].GetSprite();
            Transform bu = itemSlot.Find("Button");
            bu.name = $"{i}";
            TMP_Text priceText = bu.GetComponentInChildren<TMP_Text>();
            priceText.text = $"${price[i]}";
        }
    }
    public void OnBuy()
    {
        GameObject itemSlot = EventSystem.current.currentSelectedGameObject;
        selected = itemSlot;
        ButtonClick.Invoke(Convert.ToInt32(itemSlot.name));
    }
    public void Disable()
    {
        Button itemSlotButton = selected.GetComponent<Button>();
        itemSlotButton.interactable = false;
        itemSlotButton.enabled = false;
        itemSlotButton.colors = new ColorBlock();
        itemSlotButton.GetComponentInChildren<TMP_Text>().text = "Sold";
    }
}
