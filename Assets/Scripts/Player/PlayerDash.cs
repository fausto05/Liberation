using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashForce = 12f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private Animator animator;

    private bool isDashing;
    private bool canDash = true;

    public bool IsDashing => isDashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void TryDash()
    {
        if (!canDash || isDashing)
            return;

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        animator.SetTrigger("Dash");

        Vector2 dashDirection = playerMovement.LastMoveDirection.normalized;

        rb.linearVelocity = dashDirection * dashForce;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}