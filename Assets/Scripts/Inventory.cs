using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<GemDefinition> gems = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Add(GemDefinition gemDefinition)
    {
        if (gemDefinition != null)
            gems.Add(gemDefinition);
    }
}
