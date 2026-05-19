using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;
    public Vector2 LastMoveDirection { get; private set; } = Vector2.down;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (joystick != null)
        {
            if (Mathf.Abs(joystick.Horizontal) > 0.1f)
                moveX = joystick.Horizontal;
            if (Mathf.Abs(joystick.Vertical) > 0.1f)
                moveY = joystick.Vertical;
        }

        movement = new Vector2(moveX, moveY).normalized;

        if (movement != Vector2.zero)
            LastMoveDirection = movement;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}
