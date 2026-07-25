using UnityEngine;
using UnityEngine.InputSystem;

public class DroppedGem : MonoBehaviour
{
    public GemData gemData;
    //InputAction clickAction;

    void Start()
    {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr) && gemData.sprite != null)
        sr.sprite = gemData.sprite;
        //clickAction = InputSystem.actions.FindAction("Click");
    }

    void OnMouseDown()
    {
        if (PlayerInventory.Instance.TryAddItemData(gemData))
            Destroy(gameObject); // only destroy gameobject if gemdata was added to inventory successfully
    }
}
