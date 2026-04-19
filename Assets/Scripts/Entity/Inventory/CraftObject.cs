using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftObject : MonoBehaviour, IDropHandler
{
    private Item[] itemArray = new Item[9];
    private List<GameObject> itemImages;
    private Transform itemContainer;
    private Transform itemCraft;
    private RectTransform newItemImageTransform;
    private Image newItemImage;
    private void Awake()
    {
        itemImages = new List<GameObject>(9);
        itemContainer = transform.Find("CraftArea");
        itemCraft = itemContainer.Find("ItemCraft");
        newItemImageTransform = transform.Find("NewItem").Find("NewItemCraft").GetComponent<RectTransform>();
        newItemImage = newItemImageTransform.GetComponent<Image>();
        GenerateCraftArea();
    }
    // #############################################
    // new class/enum crafting recipes, updates, etc
    // #############################################
    public void GenerateCraftArea()
    {
        foreach (Transform child in itemContainer)
        {
            if (child.name == "ItemCraft") continue;
            GameObject.Destroy(child.gameObject);
        }
        itemImages.Clear();
        for (int i = 0; i < itemArray.Length; i++)
        {
            GameObject itemSlot = Instantiate(itemCraft.gameObject, itemContainer);
            itemSlot.name = $"ItemSlot{i}";
            itemSlot.SetActive(true);
            Image bg = itemSlot.GetComponent<Image>();
            bg.color = new Color(180, 180, 180);
            GameObject itemImage = Instantiate(itemCraft.Find("ItemImage").gameObject, itemCraft);
            itemImages.Add(itemImage);
        }
    }
    public void AddItem(Item item, int nth)
    {
        if (nth < 0 || nth > 9) return;
        // if () stackable --> stackText =+1
        itemArray[nth] = item;
        Image image = itemImages[nth].GetComponent<Image>();
        image.sprite = item.Sprite;
    }
    public void RemoveItem(Item item, int nth)
    {
        if (nth < 0 || nth > 9) return;
        // if (item.stackable) --> stackText =-1
        itemArray[nth] = null;
        Image image = itemImages[nth].GetComponent<Image>();
        image.sprite = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = newItemImageTransform.anchoredPosition;
        }
    }
    public void MergeItems()
    {
        // 4 items in container -> new
        //weapon level up
        int weaponCount = 0;
        for (int i = 0; i < itemArray.Length; i++)
        {
            if (itemArray[i].GetType() == typeof(WeaponBase)) weaponCount++;
        }
        CheckForItems();
    }
    private bool CheckForItems()
    {
        bool canCraft = false;
        int itemsCircle = 0;
        int itemsCross = 0;
        int items1st4 = 0;
        int items2nd4 = 0;
        int items3rd4 = 0;
        int items4th4 = 0;
        bool firstRow = AreItemsInRow(3);
        bool secondRow = AreItemsInRow(6);
        bool thirdRow = AreItemsInRow(9);
        for (int i = 0; i < itemArray.Length; i++)
        {
            if (itemArray[i] != null && itemArray[i].GetType() != typeof(WeaponBase))
            {
                if (i % 2 == 1) itemsCircle++;
                else itemsCross++;
            }
        }
        return canCraft;
    }
    private bool AreItemsInRow(int range)
    {
        int expected = 3;
        int count = 0;
        for (int i = range-expected; i < range; i++)
        {
            if (itemArray[i] != null && itemArray[i].GetType() != typeof(WeaponBase)) count++;
        }
        return count == expected;
    }
}