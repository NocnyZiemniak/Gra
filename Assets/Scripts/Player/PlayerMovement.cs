using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    // InputAction do ruchu
    private InputAction moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Tworzymy InputAction w kodzie
        moveAction = new InputAction("Move", InputActionType.Value, "<Keyboard>/w");

        // Dodajemy 2DCompositeBinding dla WASD
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // Gamepad analog
        moveAction.AddBinding("<Gamepad>/leftStick");

        // Callback przy zmianie wartości
        moveAction.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => movementInput = Vector2.zero;

        // Włączamy akcję
        moveAction.Enable();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }
}