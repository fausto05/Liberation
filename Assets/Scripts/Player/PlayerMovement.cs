using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;
    public Vector2 moveInput;

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

        // Si se usa joystick mobile, reemplaza teclado
        if (joystick != null)
        {
            if (Mathf.Abs(joystick.Horizontal) > 0.1f)
                moveX = joystick.Horizontal;

            if (Mathf.Abs(joystick.Vertical) > 0.1f)
                moveY = joystick.Vertical;
        }

        movement = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
