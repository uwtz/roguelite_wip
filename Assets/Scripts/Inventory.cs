using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int width = 9;
    public int height = 15;
    public InventoryGrid inventoryGrid;
    public Action OnInventoryChanged;

    protected virtual void Awake()
    {
        inventoryGrid = new InventoryGrid(width, height);
    }

    // return true if gemData was added to inventory successfully
    public bool TryAddItemData(ItemData itemData)
    {
        if (!inventoryGrid.TryFindFreeSpot(itemData, out int x, out int y))
        {
            return false;
        }

        inventoryGrid.Place(itemData, x, y);
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool TryAddItemData(ItemData itemData, int x, int y)
    {
        Debug.Log("not implemented");
        return false;
    }
}
