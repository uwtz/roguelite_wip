using UnityEngine;

// [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public abstract class ItemData : ScriptableObject
{
    [Header("Item Data")]
    public Sprite sprite;
    public int width = 1;
    public int height = 1;
}
