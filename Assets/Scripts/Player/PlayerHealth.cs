using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
    }

    public void OnDeathAnimationEnd()
    {
        GameManager.Instance.PlayerDied();
        Destroy(gameObject);
    }
}
