using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashForce = 12f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private Animator animator;
    private PlayerHealth playerHealth;

    private bool isDashing;
    private bool canDash = true;

    public bool IsDashing => isDashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TryDash()
    {
        if (playerHealth.IsDead)
            return;

        if (!canDash || isDashing)
            return;
        
        playerMovement.EnableMovement();

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("MeleeAttack");
        animator.ResetTrigger("SpecialAttack");

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

        audioSource.PlayOneShot(shootSound);
    }
}