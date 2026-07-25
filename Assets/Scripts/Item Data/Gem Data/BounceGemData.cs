using UnityEngine;

[CreateAssetMenu(fileName = "BounceGemData", menuName = "Scriptable Objects/GemData/BounceGemData")]
public class BounceGemData : GemData
{
    public int count;
    void OnEnable()
    {
        type = GemType.Bounce;
    }
}
