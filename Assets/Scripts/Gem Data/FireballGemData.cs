using UnityEngine;

[CreateAssetMenu(fileName = "FireballGemData", menuName = "Scriptable Objects/GemData/FireballGemData")]
public class FireballGemData : GemData
{
    void OnEnable()
    {
        type = GemType.Fireball;
    }
}
