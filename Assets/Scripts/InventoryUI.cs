using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject cellPrefab;
    private Image[,] gridUI;

    void Awake()
    {
        if (TryGetComponent<GridLayoutGroup>(out GridLayoutGroup glg))
            glg.constraintCount = inventory.width;

        gridUI = new Image[inventory.width, inventory.height];

        for (int y=0; y<inventory.height; y++)
            for (int x=0; x<inventory.width; x++)
            {
                GameObject cell = Instantiate(cellPrefab, transform);
                cell.name = $"{cellPrefab.name} ({x}, {y})";
                gridUI[x,y] = cell.GetComponent<Image>();
            }

        inventory.OnInventoryChanged += UpdateGridUI;
    }

    void UpdateGridUI()
    {
        for (int x=0; x<inventory.width; x++)
            for (int y=0; y<inventory.height; y++)
            {
                PlacedItem placedItem = inventory.inventoryGrid.GetItem(x,y);
                gridUI[x,y].sprite = placedItem?.itemData.sprite;

                if (placedItem.x == x && placedItem.y == y)
                    CreateItemUI(placedItem);
            }
    }

    void CreateItemUI(PlacedItem placedItem)
    {
        
    }
}
