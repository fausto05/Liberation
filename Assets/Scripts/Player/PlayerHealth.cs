using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    private int maxHealth = 100;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isDead = false;

    private Color originalColor;
    private Color grayColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Guarda el color naranja original que tengas puesto en el Inspector
        originalColor = spriteRenderer.color;
    }

    private void Start()
    {
        SaveData data = SaveSystem.Load();

        health = data.playerHealth;

        health = Mathf.Clamp(health, 1, maxHealth);

        UpdateSpriteColor();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        SaveData data = SaveSystem.Load();

        data.playerHealth = health;

        SaveSystem.Save(data);

        UpdateSpriteColor();

        if (health <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
    }

    private void UpdateSpriteColor()
    {
        // 1 = vida completa, 0 = sin vida
        float healthPercent = (float)health / maxHealth;

        // Con 100 HP mantiene el color original.
        // Con 0 HP queda gris.
        spriteRenderer.color = Color.Lerp(grayColor, originalColor, healthPercent);
    }

    public void OnDeathAnimationEnd()
    {
        GameManager.Instance.PlayerDied();
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        if (isDead)
            return;

        health += amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        SaveData data = SaveSystem.Load();

        data.playerHealth = health;

        SaveSystem.Save(data);

        UpdateSpriteColor();
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
