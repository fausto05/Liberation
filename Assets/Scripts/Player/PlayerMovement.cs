using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;

    public Vector2 LastMoveDirection { get; private set; } = Vector2.down;

    private Rigidbody2D rb;

    private Vector2 keyboardMovement;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        keyboardMovement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        movement = keyboardMovement;

       
        if (joystick != null)
        {
            Vector2 joystickInput = new Vector2(
                joystick.Horizontal,
                joystick.Vertical
            );

            if (joystickInput.magnitude > 0.1f)
            {
                movement = joystickInput.normalized;
            }
        }

        if (movement != Vector2.zero)
            LastMoveDirection = movement;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}
