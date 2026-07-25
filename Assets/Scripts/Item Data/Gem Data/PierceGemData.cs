using UnityEngine;

[CreateAssetMenu(fileName = "PierceGemData", menuName = "Scriptable Objects/GemData/PierceGemData")]
public class PierceGemData : GemData
{
    public int count;
    void OnEnable()
    {
        type = GemType.Pierce;
    }
}
