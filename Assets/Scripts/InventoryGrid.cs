using UnityEngine;

public class InventoryGrid
{
    public int width;
    public int height;
    private readonly PlacedItem[,] grid;

    public InventoryGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new PlacedItem[width, height];
    }

    public PlacedItem GetItem(int x, int y)
    {
        return grid[x,y];
    }

    public bool CanPlace(ItemData itemData, int x, int y)
    {
        int w = itemData.width;
        int h = itemData.height;

        if (x<0 || y<0 || x+w>width || y+h>height)
            return false;
        
        for (int i=x; i<x+w; i++)
            for (int j=y; j<y+h; j++)
                if (grid[i,j] != null)
                    return false;
        
        return true;
    }

    public PlacedItem Place(ItemData itemData, int x, int y)
    {
        if (!CanPlace(itemData, x, y))
            return null;
        
        var placedItem = new PlacedItem(itemData, x, y);
        int w = itemData.width;
        int h = itemData.height;

        for (int i=x; i<x+w; i++)
            for (int j=y; j<y+h; j++)
                grid[i, j] = placedItem;

        return placedItem;
    }

    public void Remove(PlacedItem placedItem)
    {
        for (int i=placedItem.x; i<placedItem.x + placedItem.itemData.width; i++)
            for (int j=placedItem.y; j<placedItem.y + placedItem.itemData.height; j++)
                grid[i,j] = null;

        // might need to parse through all cell and check if item matches
        // this way an item already removed can be used to call Remove() and remove something else instead
    }

    public bool TryFindFreeSpot(ItemData itemData, out int foundX, out int foundY)
    {
        int w = itemData.width;
        int h = itemData.height;

        for (int i=0; i<width; i++)
            for (int j=0; j<height; j++)
                if (CanPlace(itemData, i, j))
                {
                    foundX = i;
                    foundY = j;
                    return true;
                }
        
        Debug.Log($"no inv space for item sized {w} x {h}");
        foundX = -1;
        foundY = -1;
        return false;
    }
}

public class PlacedItem
{
    public ItemData itemData;
    public int x, y;

    public PlacedItem(ItemData itemData, int x, int y)
    {
        this.itemData = itemData;
        this.x = x;
        this.y = y;
    }
}
