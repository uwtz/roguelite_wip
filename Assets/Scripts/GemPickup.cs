using UnityEngine;
using UnityEngine.InputSystem;

public class GemPickup : MonoBehaviour
{
    public GemData gemData;
    //InputAction clickAction;

    void Start()
    {
        //clickAction = InputSystem.actions.FindAction("Click");
    }

    void OnMouseDown()
    {
        if (Inventory.Instance.Add(gemData))
            Destroy(gameObject); // only destroy gameobject if gemdata was added to inventory successfully
    }
}
