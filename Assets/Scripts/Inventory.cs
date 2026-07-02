using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<GemData> gemDatas = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(gameObject); return; }
        Instance = this;
    }

    // return true if gemData was added to inventory successfully
    public bool Add(GemData gemData)
    {
        // TODO: check if inventory is full. if so dont add to inv and ret false
        if (gemData != null)
        {
            gemDatas.Add(gemData);
            return true;
        }
        return false;
    }
}
