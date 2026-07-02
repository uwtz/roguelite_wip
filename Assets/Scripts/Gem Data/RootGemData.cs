using UnityEngine;

[CreateAssetMenu(fileName = "RootGemData", menuName = "Scriptable Objects/GemData/RootGemData")]
public class RootGemData : GemData
{
    void OnEnable()
    {
        type = GemType.Root;
    }
}
