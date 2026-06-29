using UnityEngine;

//[CreateAssetMenu(fileName = "GemData", menuName = "Scriptable Objects/GemData/GemData")]
public abstract class GemData : ScriptableObject
{
    public GemType type;
    public int maxChildren;
}

[CreateAssetMenu(fileName = "RootGemData", menuName = "Scriptable Objects/GemData/RootGemData")]
public class RootGemData : GemData
{
    
}

[CreateAssetMenu(fileName = "FireballGemData", menuName = "Scriptable Objects/GemData/FireballGemData")]
public class FireballGemData : GemData
{
    
}

[CreateAssetMenu(fileName = "PierceGemData", menuName = "Scriptable Objects/GemData/PierceGemData")]
public class PierceGemData : GemData
{
    public int count;
}

[CreateAssetMenu(fileName = "VolleyGemData", menuName = "Scriptable Objects/GemData/VolleyGemData")]
public class VolleyGemData : GemData
{
    public int count;
}