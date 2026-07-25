using UnityEngine;

[CreateAssetMenu(fileName = "VolleyGemData", menuName = "Scriptable Objects/GemData/VolleyGemData")]
public class VolleyGemData : GemData
{
    public int count;
    public int spread;
    void OnEnable()
    {
        type = GemType.Volley;
    }
}
