using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftObject : MonoBehaviour, IPointerClickHandler
{
    //container transform
    [SerializeField] private Canvas canvas;
    private Transform itemContainer;
    private Transform itemCraft;
    private Transform itemInventory;
    private float size = 65f;
    // items in transform
    [SerializeField] private DragNDrop dndObject;
    private Item selectedItem = null;
    //inventory
    private Inventory inventory;
    public Inventory Inventory { get => inventory; set => inventory = value; }
    //craftarea
    private Item[] itemArray = new Item[9];
    private List<Transform> itemImages;
    //newitem
    private RectTransform newItemImageTransform;
    private Image newItemImage;

    private void Awake()
    {
        itemImages = new List<Transform>(9);
        itemContainer = transform;
        itemInventory = itemContainer.Find("InventoryItemTemplate");
        itemCraft = itemContainer.Find("CraftAreaItemTemplate");

        newItemImageTransform = transform.Find("NewItem").Find("NewItemCraft").GetComponent<RectTransform>();
        newItemImage = newItemImageTransform.GetComponent<Image>();

        GenerateCraftArea();
        GenerateInventory();
    }
    // #############################################
    // new class/enum crafting recipes, updates, etc
    // #############################################
    private void GenerateCraftArea()
    {
        foreach (Transform child in itemContainer)
        {
            if (child.name.Contains("ItemCraftSlot")) GameObject.Destroy(child.gameObject);
        }
        itemImages.Clear();
        int x = 0, y = 0;
        for (int i = 0; i < itemArray.Length; i++)
        {
            Transform itemSlot = Instantiate(itemCraft, itemContainer);
            itemSlot.name = $"ItemCraftSlot{i}";
            itemSlot.gameObject.SetActive(true);
            Image bg = itemSlot.GetComponent<Image>();
            //bg.color = new Color(180, 180, 180);
            Transform itemImage = itemSlot.Find("CraftImage");
            Transform sdf = itemSlot.Find("PlaceHolder");
            DragNDropItem dnd = sdf.GetOrAddComponent<DragNDropItem>();
            itemImages.Add(itemImage);
            itemSlot.position = new Vector2(x * size, y * size*-1) + (Vector2)itemCraft.position;
            x++;
            if (x > 2)
            {
                x = 0;
                y++;
            }
        }
    }
    private void GenerateInventory()
    {
        foreach (Transform child in itemContainer)
        {
            if (child.name.Contains("ItemIventorySlot")) GameObject.Destroy(child.gameObject);
        }
        int x = 0, y = 0;
        for (int i = 0; i < inventory.GetItems().Count; i++)
        {
            if (y >= 3) break;
            Transform itemSlot = Instantiate(itemInventory, itemContainer);
            itemSlot.name = $"ItemIventorySlot{i}";
            itemSlot.gameObject.SetActive(true);
            Image bg = itemSlot.GetComponent<Image>();
            //bg.color = new Color(180, 180, 180);
            Transform itemImage = itemSlot.Find("ItemImage");
            Transform sdf = itemSlot.Find("PlaceHolder");
            DragNDropItem di = sdf.GetOrAddComponent<DragNDropItem>();
            //DragNDropItem di = itemImage.GetComponentInChildren<DragNDropItem>();
            //di.Item = inventory.GetItems()[i];
            DragNDrop dnd = itemImage.GetOrAddComponent<DragNDrop>();
            dnd.Item = inventory.GetItems()[i];
            dnd.SetCanvas(canvas);
            itemImage.GetComponent<Image>().sprite = inventory.GetItems()[i].GetSprite();

            //itemImage.Find("Renderer").GetComponent<SpriteRenderer>().sprite = inventory.GetItems()[i].GetSprite();
            itemSlot.position = new Vector2(x * size, y * size * -1) + (Vector2)itemInventory.position;
            x++;
            if (x > 8)
            {
                x = 0;
                y++;
            }
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerClick != null)
        {
            GameObject pointed = eventData.pointerClick;
            DragNDropItem di = pointed.gameObject.GetComponentInChildren<DragNDropItem>();
            if (selectedItem == null) {
                selectedItem = di.Item;
                di.Item = null;
                //selectedItem = pointed.gameObject.GetComponentInChildren<SpriteRenderer>
            }
            else if (di.Item == null)
            {
                di.Item = selectedItem;
                selectedItem = null;
            }
            //Debug.Log(di.Item);
        }
    }
    public void MergeItems()
    {
        //list
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
            switch (i)
            {
                case 0: items1st4++; break;
                case 1: items1st4++; items2nd4++; break;
                case 2: items2nd4++; break;
                case 3: items1st4++; items3rd4++; break;
                case 4: items1st4++; items2nd4++; items3rd4++; items4th4++; break;
                case 5: items2nd4++; items4th4++; break;
                case 6: items3rd4++; break;
                case 7: items3rd4++; items4th4++; break;
                case 8: items4th4++; break;
                default: break;
            }
        }
        if (items1st4 == 4 || items2nd4 == 4 || items3rd4 == 4 || items4th4 == 4 || itemsCircle == 4) canCraft = true;
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