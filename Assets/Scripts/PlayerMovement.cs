using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;

    [SerializeField] int spd = 1000;
    private Vector2 moveVector;
    private Rigidbody2D rb;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 tmp = moveAction.ReadValue<Vector2>();
        if (tmp != moveVector)
        {
            moveVector = tmp;
            //Debug.Log(moveVector);
        }
        rb.linearVelocity = spd * Time.deltaTime * moveVector;
    }
}
