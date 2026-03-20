using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementNewInput : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 movement;

    void Update()
    {
        // Odczyt inputu wprost z klawiatury / gamepada
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            movement = Vector2.zero;

            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
                movement.y += 1;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
                movement.y -= 1;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                movement.x -= 1;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                movement.x += 1;

            movement = movement.normalized;
        }

        // Ruch gracza
        transform.position += (Vector3)movement * moveSpeed * Time.deltaTime;
    }
}