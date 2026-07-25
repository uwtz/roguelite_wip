using UnityEngine;

public class PlayerInventory : Inventory
{
    public static PlayerInventory Instance {get; private set;}

    protected override void Awake()
    {
        base.Awake();

        if (Instance != null && Instance != this)
        { Destroy(gameObject); return; }
        Instance = this;
    }
}
