using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;

    private InputAction moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Tworzymy InputAction w kodzie
        moveAction = new InputAction("Move", InputActionType.Value);

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
        // Ruch fizyczny
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);

        // Animacje
        UpdateAnimation(movementInput);
    }

    private void UpdateAnimation(Vector2 move)
    {
        if (animator == null) return;

        if (move != Vector2.zero)
        {
            animator.SetFloat("Inputx", move.x);
            animator.SetFloat("Inputy", move.y);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastInputx", move.x);
            animator.SetFloat("IsWalking", move.y);
        }
    }
}