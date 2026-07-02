using UnityEngine;

// obsolete
[CreateAssetMenu(fileName = "GemDefinition", menuName = "Scriptable Objects/GemDefinition")]
public class GemDefinition : ScriptableObject
{
    public GemType type;
    public Sprite sprite;
    public string description;
    public int maxChildren;
}
