using UnityEngine;

/*
a monobehavior script that holds references to prefabs that gems need.
instantiation of gems that requires prefabs goes through here
(as opposed to getting reference to prefabs using Resources.Load<>() in Gem.cs)
*/
public class GemHelper : MonoBehaviour
{
    // singleton
    public static GemHelper Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(gameObject); return; }
        Instance = this;
    }

    [SerializeField] GameObject FireballPrefab;

    public Gem CreateFireball(FireballGemData fireballGemData)
    {
        return new Fireball(FireballPrefab, fireballGemData);
    }
}
