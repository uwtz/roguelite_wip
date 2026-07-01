using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponTreeData", menuName = "Scriptable Objects/WeaponTreeData")]
public class WeaponTreeData : ScriptableObject
{
    //public WeaponTreeNodeData rootWeaponTreeNodeData;// = new WeaponTreeNodeData{ gem = new GemData { name = "Root" } };
    public WeaponTreeNodeData[] weaponTreeNodeDatas;
}

[System.Serializable]
public class WeaponTreeNodeData
{
    public GemData gem;
    //public WeaponTreeNodeData[] children;
    public int[] childIndices;
}

/*
[System.Serializable]
public class GemData
{
    public GemId id;
    public int maxChildren;
}*/