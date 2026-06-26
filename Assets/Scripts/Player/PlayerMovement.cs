using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;
    
    public bool CanMove { get; set; } = true;

    public Vector2 LastMoveDirection { get; private set; } = Vector2.down;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 keyboardMovement;
    private Vector2 movement;
    private PlayerDash playerDash;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerDash = GetComponent<PlayerDash>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        keyboardMovement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (!CanMove)
        {
            movement = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }

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

        if (movement.x < 0)
            spriteRenderer.flipX = true;
        else if (movement.x > 0)
            spriteRenderer.flipX = false;

        animator.SetBool("isMoving", movement != Vector2.zero);
    }

    public void SetFacingDirection(Vector2 direction)
    {
        if (direction.x < 0)
            spriteRenderer.flipX = true;
        else if (direction.x > 0)
            spriteRenderer.flipX = false;
    }

    private void FixedUpdate()
    {
        if (!CanMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (!playerDash.IsDashing)
        {
            rb.linearVelocity = movement * moveSpeed;
        }
    }

    public void EnableMovement()
    {
        CanMove = true;
    }
}
