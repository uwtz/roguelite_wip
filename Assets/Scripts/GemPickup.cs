using UnityEngine;
using UnityEngine.InputSystem;

public class GemPickup : MonoBehaviour
{
    public GemDefinition gemDefinition;
    //InputAction clickAction;

    void Start()
    {
        //clickAction = InputSystem.actions.FindAction("Click");
    }

    void OnMouseDown()
    {
        Inventory.Instance.Add(gemDefinition);
        Destroy(gameObject);
    }
}
