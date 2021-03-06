using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    
    [SerializeField] List<ItemUI> itemsUIList = new List<ItemUI>();
    //Dictionary<Type, Item> items = new Dictionary<Type, Item>();
    List<Item> itemsIventory = new List<Item>();
    [SerializeField] CraftingUI ui;

    private void Awake()
    {
        //Get all the itemUIs
        GetComponentsInChildren<ItemUI>(itemsUIList);
        foreach (ItemUI item in itemsUIList)
        {
            ChangeAlpha(0, item);
        }
    }

    public void AddItem(Item item) 
    {
        if (IsFull()) return;

        Type itemType = item.GetType();

        if (itemType == typeof(Material))
        {
            foreach (Item items in itemsIventory)
            {               
                if (items.CanAdd())
                {                    
                    if (items.Equals(item))
                    {
                        items.Add(1);
                        Destroy(item.gameObject);
                        return;
                    }
                }          
            }                 
            itemsIventory.Add(AddToUI(item as Material));
            Debug.Log("added material");
            return;
        }
        if (itemType == typeof(Armor))
        {
            itemsIventory.Add(AddToUI(item as Armor));
            return;
        }
    }

    Item AddToUI(Armor item)
    {
        foreach (ItemUI itemui in itemsUIList)
        {
            if (!itemui.hasItem)
            {
                ChangeAlpha(1, itemui);
                itemui.hasItem = true;
                itemui.UpdateUI(item);
                return CopyItem(item, itemui);
            }
        }
        return null;
    }
    Item AddToUI(Material item)
    {

        foreach (ItemUI itemui in itemsUIList)
        {
            if (!itemui.hasItem)
            {
                ChangeAlpha(1, itemui);
                itemui.hasItem = true;
                itemui.UpdateUI(item);
                return CopyItem(item, itemui);
            }
        }
        return null;
    }
    Item CopyItem(Armor _item, ItemUI itemUI)
    {
        Armor newItem = itemUI.gameObject.AddComponent<Armor>();
        newItem.ItemName = _item.ItemName;
        newItem.Icon = _item.Icon;
        newItem.Description = _item.Description;
        newItem.Value = _item.Value;
        newItem.Weight = _item.Weight;
        newItem.ArmorSprite = _item.ArmorSprite;
        newItem.Slot = _item.Slot;
        Destroy(_item.gameObject);
        return newItem;
    }
    Item CopyItem(Material _item, ItemUI itemUI)
    {
        Material newItem = itemUI.gameObject.AddComponent<Material>();
        newItem.ItemName = _item.ItemName;
        newItem.AmountOwned = _item.AmountOwned;
        newItem.MaxCapacity = _item.MaxCapacity;
        newItem.Icon = _item.Icon;
        newItem.Description = _item.Description;
        newItem.Value = _item.Value;
        newItem.Weight = _item.Weight;
        newItem.Type = _item.Type;
        Destroy(_item.gameObject);
        return newItem;
    }

    void ChangeAlpha(int alpha, ItemUI item)
    {
        Color color;
        color = item.image.color;
        color.a = alpha;
        item.image.color = color;
    }

    //public bool ContainsItems(List<Item> item)
    //{
    //    foreach (Item item1 in item)
    //    {
    //        if (!itemsIventory.Contains(item1))
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
    public bool ContainsItem(Item item)
    {
        foreach (Item items in itemsIventory)
        {
            if (items.Equals(item))
            {
                return true;
            }
        }
        return false;
    }
    public void RemoveItems(List<Item> items)
    {
        foreach (Item itemToRemove in items)
        {
            Type itemType = itemToRemove.GetType();
            foreach (Item item1 in itemsIventory)
            {             
                if (item1.Equals(itemToRemove))
                {
                    if (itemType == typeof(Material))
                    {
                        Material item = itemToRemove as Material;
                        if (item1.Use(item.AmountOwned) == 0)
                        {
                            RemoveFromInventory(item1);
                        }
                    }
                    else
                    {
                        RemoveFromInventory(item1);
                    }
                }
                ui.RemoveFromMaterials(item1);
                item1.GetComponent<ItemUI>().ResetColor();
            }
        }
    }
    public void RemoveItem(Item item)
    {
        Type itemType = item.GetType();
        if (itemsIventory.Contains(item))
        {
            if (itemType == typeof(Material))
            {
                Material itemMaterial = item as Material;
                if (item.Use(itemMaterial.AmountOwned) == 0)
                {
                    RemoveFromInventory(item);
                }
            }
        }
        else { RemoveFromInventory(item); }
        ui.RemoveFromMaterials(item);
        item.GetComponent<ItemUI>().ResetColor();
    }
    public bool IsFull()
    {
        if (itemsIventory.Count >= 12)
        {
            return true;
        }
        return false;
    }

    void RemoveFromInventory(Item item1)
    {
        if (item1.TryGetComponent<ItemUI>(out ItemUI itemui))
        {
            ChangeAlpha(0, itemui);
            itemui.ResetItemUI();
            Destroy(item1);
        }
    }
}
